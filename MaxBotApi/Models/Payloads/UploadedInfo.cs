using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class UploadedInfo
{
    /// <summary>
    /// Токен — уникальный ID загруженного медиафайла
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}