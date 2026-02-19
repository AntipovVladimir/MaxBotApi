using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class FileAttachmentPayload
{
    /// <summary>
    /// URL медиа-вложения. Этот URL будет получен в объекте Update после отправки сообщения в чат.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    
    /// <summary>
    /// Используйте token, если вы пытаетесь повторно использовать одно и то же вложение в другом сообщении.
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }
}