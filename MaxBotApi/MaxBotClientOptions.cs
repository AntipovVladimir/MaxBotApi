using System.Globalization;
using System.Runtime.CompilerServices;

namespace MaxBotApi;

public class MaxBotClientOptions
{
    public const string BaseMaxUri = "https://platform-api.max.ru";

    /// <summary>API token</summary>
    public string Token { get; }
    
    /// <summary>Automatic retry of failed requests "Too Many Requests: retry after X" when X is less or equal to RetryThreshold</summary>
    public int RetryThreshold { get; set; } = 60;

    /// <summary><see cref="RetryThreshold">Automatic retry</see> will be attempted for up to RetryCount requests</summary>
    public int RetryCount { get; set; } = 3; 
    
    
    public MaxBotClientOptions(string token)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }
 
}