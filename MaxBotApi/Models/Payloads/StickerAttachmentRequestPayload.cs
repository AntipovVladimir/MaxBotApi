using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models.Payloads;

public class StickerAttachmentRequestPayload
{
    /// <summary>
    /// Код стикера
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; set; }
    public override string ToString() => this.SerializeToString();
}