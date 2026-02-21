namespace MaxBotApi.Exceptions;

/// <summary>Represents an API error</summary>
public class ApiRequestException : RequestException
{
    /// <summary>Initializes a new instance of the <see cref="ApiRequestException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ApiRequestException(string message) : base(message)
    {
    }
}