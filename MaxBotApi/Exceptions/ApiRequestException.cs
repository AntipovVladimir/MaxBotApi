namespace MaxBotApi.Exceptions;

/// <summary>Represents an API error</summary>
public class ApiRequestException : RequestException
{
    public string Code { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ApiRequestException"/> class.</summary>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <param name="code">Код ошибки</param>
    public ApiRequestException(string message, string code = "") : base(message)
    {
        Code = code;
    }
}