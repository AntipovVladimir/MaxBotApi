using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using MaxBotApi.Exceptions;
using MaxBotApi.Interfaces;
using MaxBotApi.Serialization;
using MaxBotApi.Types;

namespace MaxBotApi;

public class MaxBotClient : IMaxBotClient
{
    private readonly MaxBotClientOptions _options;
    //private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    /// <summary>Global cancellation token</summary>
    public CancellationToken GlobalCancelToken { get; }

    private readonly HttpClient _httpClient;

    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    /// <summary>Create a new <see cref="MaxBotClient"/> instance.</summary>
    /// <param name="options">Configuration for <see cref="MaxBotClient" /></param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="options"/> is <see langword="null"/></exception>
    public MaxBotClient(MaxBotClientOptions options, HttpClient? httpClient = null, CancellationToken cancellationToken = default)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(3) });
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        GlobalCancelToken = cancellationToken;
    }

    public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();


    /// <summary>Create a new <see cref="MaxBotClient"/> instance.</summary>
    /// <param name="token">The bot token</param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="token"/> format is invalid</exception>
    public MaxBotClient(string token, HttpClient? httpClient = null, CancellationToken cancellationToken = default)
        : this(new MaxBotClientOptions(token), httpClient, cancellationToken)
    {
    }

    private static readonly Encoding Latin1 = Encoding.GetEncoding(28591);

    public virtual async Task<TResponse> SendFile<TResponse>(FileRequestBase<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        cancellationToken = cts.Token;
        string url = request.MethodName;
        string filename = Path.GetFileName(request.FileName);
        string contentDisposition = FormattableString.Invariant($"form-data; name=\"data\"; filename=\"{filename}\"");
        contentDisposition = Latin1.GetString(Encoding.UTF8.GetBytes(contentDisposition));
        await using var fs = File.OpenRead(request.FileName);
        var fileContent = new StreamContent(fs)
        {
            Headers =
            {
                { "Content-Type", "application/octet-stream" },
                { "Content-Disposition", contentDisposition },
            }
        };


        var content = new MultipartFormDataContent();
        content.Add(fileContent);
        var httpRequest = new HttpRequestMessage(request.HttpMethod, url) { Content = content };
        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        }
        catch (TaskCanceledException exception)
        {
            if (cancellationToken.IsCancellationRequested) throw;
            throw new RequestException("CDN Request timed out", exception);
        }
        catch (Exception exception)
        {
            throw new RequestException(string.Format("CDN Service Failure: {0}: {1}", exception.GetType().Name, exception.Message), exception);
        }

        for (int attempt = 1;; attempt++)
        {
            using (httpResponse)
            {
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                        throw new RequestException(string.Format("CDN returns BadRequest while uploading to url {0}", url));
                    var failedApiResponse = await DeserializeContent<ApiResponse>(httpResponse,
                        response => !response.Success && response.Message != null, cancellationToken).ConfigureAwait(false);

                    if (httpResponse.StatusCode == HttpStatusCode.TooManyRequests && _options.RetryThreshold > 0 && attempt < _options.RetryCount)
                    {
                        await Task.Delay(5 * 1000, cancellationToken).ConfigureAwait(false);
                        continue; // retry attempt
                    }

                    throw ExceptionsParser.Parse(failedApiResponse);
                }

                TResponse? deserializedObject = default;
                string response = string.Empty;
                try
                {
                    response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TResponse));
                    deserializedObject = (TResponse)xmlSerializer.Deserialize(new StringReader(response))!;
                }
                catch (Exception exception)
                {
                    throw new RequestException(string.Format("There was an exception during deserialization of the response: {0}", response),
                        httpResponse.StatusCode, exception);
                }

                return deserializedObject!;
            }
        }
    }


    public virtual async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        cancellationToken = cts.Token;
        string url = string.Format("{0}/{1}", MaxBotClientOptions.BaseMaxUri, request.MethodName);
        using var httpContent = request.ToHttpContent();
        for (int attempt = 1;; attempt++)
        {
            var httpRequest = new HttpRequestMessage(request.HttpMethod, url) { Content = httpContent };

            httpRequest.Headers.Add("Authorization", _options.Token);
            string? message = httpRequest.Content?.ReadAsStringAsync().Result;

            // httpContent.Headers.ContentLength must be called after OnMakingApiRequest, because it enforces the
            // final ContentLength header, and OnMakingApiRequest might modify the content, leading to discrepancy
            if (httpContent != null && _options.RetryThreshold > 0 && _options.RetryCount > 1 && httpContent.Headers.ContentLength == null)
                await httpContent.LoadIntoBufferAsync().ConfigureAwait(false);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested) throw;
                throw new RequestException("Bot API Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException(string.Format("Bot API Service Failure: {0}: {1}", exception.GetType().Name, exception.Message), exception);
            }

            using (httpResponse)
            {
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var failedApiResponse = await DeserializeContent<ApiResponse>(httpResponse,
                        response => !response.Success && response.Message != null, cancellationToken).ConfigureAwait(false);

                    if (httpResponse.StatusCode == HttpStatusCode.TooManyRequests && _options.RetryThreshold > 0 && attempt < _options.RetryCount)
                    {
                        await Task.Delay(5 * 1000, cancellationToken).ConfigureAwait(false);
                        continue; // retry attempt
                    }

                    throw ExceptionsParser.Parse(failedApiResponse);
                }

                TResponse? deserializedObject;
                string response = string.Empty;
                try
                {
                    response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                    deserializedObject = JsonSerializer.Deserialize<TResponse>(response, JsonBotAPI.Options);
                }
                catch (Exception exception)
                {
                    throw new RequestException(string.Format("There was an exception during deserialization of the response: {0}", response),
                        httpResponse.StatusCode, exception);
                }

                return deserializedObject!;
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<T> DeserializeContent<T>(HttpResponseMessage httpResponse, Func<T, bool> validate,
        CancellationToken cancellationToken = default) where T : class
    {
        if (httpResponse.Content is null)
            throw new RequestException("Response doesn't contain any content", httpResponse.StatusCode);
        T? deserializedObject;
        string response = string.Empty;
        try
        {
            response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            deserializedObject = JsonSerializer.Deserialize<T>(response, JsonBotAPI.Options);
        }
        catch (Exception exception)
        {
            throw new RequestException(string.Format("There was an exception during deserialization of the response: {0}", response), httpResponse.StatusCode,
                exception);
        }

        if (deserializedObject is null || !validate(deserializedObject))
            throw new RequestException("Required properties not found in response", httpResponse.StatusCode);
        return deserializedObject;
    }
}