namespace MaxBotApi;

public class MaxBotClientOptions
{
    public const string BaseMaxUri = "https://platform-api.max.ru";

    /// <summary>
    /// API токен
    /// </summary>
    public string Token { get; }
    
    /// <summary>
    /// Автоматический повтор запроса при получении ошибки "Too Many Requests: retry after X" где X меньше или равно RetryThreshold
    /// </summary>
    public int RetryThreshold { get; set; } = 60;

    /// <summary>
    /// Автоматический повтор запроса будет делать RetryCount попыток
    /// </summary>
    public int RetryCount { get; set; } = 3; 
    
    
    public MaxBotClientOptions(string token)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }
 
}