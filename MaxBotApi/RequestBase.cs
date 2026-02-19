using System.Text.Json.Serialization;
using MaxBotApi.Interfaces;
using MaxBotApi.Serialization;

namespace MaxBotApi;

public abstract class RequestBase<TResponse>(string methodName) : IRequest<TResponse>
{
    [JsonIgnore]
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

    [JsonIgnore]
    public string MethodName { get; } = methodName;

    [JsonIgnore]
    public bool IsWebhookResponse { get; set; }

    [JsonInclude]
    internal string? Method => IsWebhookResponse ? MethodName : null;

    /// <summary>Generate content of HTTP message</summary>
    /// <returns>Content of HTTP request</returns>
    public virtual HttpContent? ToHttpContent() =>
        System.Net.Http.Json.JsonContent.Create(this, GetType(), options: JsonBotAPI.Options);
}

/// <summary>Represents a request that doesn't require any parameters</summary>
/// <param name="methodName">Name of request method</param>
public abstract class ParameterlessRequest<TResult>(string methodName) : RequestBase<TResult>(methodName)
{
    /// <inheritdoc/>
    public override HttpContent? ToHttpContent() => IsWebhookResponse ? base.ToHttpContent() : null;
}

