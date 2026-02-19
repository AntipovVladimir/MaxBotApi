using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class StickerAttachmentRequestPayload
{
    /// <summary>
    /// Код стикера
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; set; }
}