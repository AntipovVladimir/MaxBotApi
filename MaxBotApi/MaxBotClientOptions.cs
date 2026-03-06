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
    /// Автоматический повтор запроса будет делать RetryCount попыток, каждая новая попытка будет через 5 секунд. 
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// Автоматический повтор запроса, если получен ответ что вложения еще не готовы\обрабатываются. Для SendMessage после UploadFile.
    /// каждая следующая попытка будет через X * 5 секунд, где X - номер попытки. Суммарно не более 275сек 
    /// </summary>
    public int RetryWaitAttachment { get; set; } = 10;
    
    public MaxBotClientOptions(string token)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }
 
}