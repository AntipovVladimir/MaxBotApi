


using MaxBotApi.Exceptions;
using MaxBotApi.Interfaces;


public interface IMaxBotClient
{
    /// <summary>Instance of <see cref="IExceptionParser"/> to parse errors from Bot API into <see cref="ApiRequestException"/></summary>
    /// <remarks>This property is not thread safe</remarks>
    IExceptionParser ExceptionsParser { get; set; }

    /// <summary>Timeout for requests</summary>
    TimeSpan Timeout { get; set; }
    /// <summary>Send a request to Bot API</summary>
    /// <typeparam name="TResponse">Type of expected result in the response object</typeparam>
    /// <param name="request">API request object</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result of the API request</returns>
    Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}