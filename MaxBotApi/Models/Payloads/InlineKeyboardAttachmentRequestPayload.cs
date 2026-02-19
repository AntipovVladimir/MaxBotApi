using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class InlineKeyboardAttachmentRequestPayload
{
    /// <summary>
    /// Двумерный массив кнопок
    /// </summary>
    [JsonPropertyName("buttons")]
    public required IEnumerable<IEnumerable<Button>> Buttons { get; set; }
}