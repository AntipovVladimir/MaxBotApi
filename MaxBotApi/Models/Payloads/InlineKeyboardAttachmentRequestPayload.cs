using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models.Payloads;

public class InlineKeyboardAttachmentRequestPayload
{
    /// <summary>
    /// Двумерный массив кнопок
    /// </summary>
    [JsonPropertyName("buttons")]
    public required IEnumerable<IEnumerable<Button>> Buttons { get; set; }
    public override string ToString() => this.SerializeToString();
}