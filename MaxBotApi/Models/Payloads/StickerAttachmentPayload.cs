using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class StickerAttachmentPayload
{
    /// <summary>
    /// URL медиа-вложения. Этот URL будет получен в объекте Update после отправки сообщения в чат.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    
    /// <summary>
    /// ID стикера
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; set; }
}