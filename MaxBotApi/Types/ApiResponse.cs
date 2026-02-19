using System.Text.Json.Serialization;

namespace MaxBotApi.Types;

/// <summary>Ответ от API на запрос действия</summary>
public class ApiResponse
{
    /// <summary>
    /// true, если запрос был успешным, false в противном случае
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Объяснительное сообщение, если результат не был успешным
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}