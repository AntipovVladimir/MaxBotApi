namespace MaxBotApi.Exceptions;

/// <summary>Represents an API error</summary>
#pragma warning disable CA1032
public class ApiRequestException : RequestException
#pragma warning restore CA1032
{
    /// <summary>Initializes a new instance of the <see cref="ApiRequestException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ApiRequestException(string message) : base(message)
    {
    }
}