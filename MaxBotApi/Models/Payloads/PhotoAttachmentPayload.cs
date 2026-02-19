using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class PhotoAttachmentPayload
{
    /// <summary>
    /// Уникальный ID этого изображения
    /// </summary>
    [JsonPropertyName("photo_id")]
    public long PhotoId { get; set; }

    [JsonPropertyName("token")] public string Token { get; set; } = string.Empty;

    /// <summary>
    /// URL изображения
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}