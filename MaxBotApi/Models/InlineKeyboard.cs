using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Models;

public partial class InlineKeyboard
{
    /// <summary>
    /// двумерный массив кнопок ([строка][столбец])
    /// </summary>
    [JsonPropertyName("buttons")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<IEnumerable<Button>> Buttons { get; set; } = [];

    [SetsRequiredMembers]
    public InlineKeyboard(IEnumerable<IEnumerable<Button>> buttons) => Buttons = buttons;

    [SetsRequiredMembers]
    public InlineKeyboard() => Buttons = new List<List<Button>>();

    public static explicit operator InlineKeyboardAttachment(InlineKeyboard ik) => new() { Payload = ik };

    public static explicit operator InlineKeyboardAttachmentRequest(InlineKeyboard ik) => new()
    {
        Payload = new InlineKeyboardAttachmentRequestPayload() { Buttons = ik.Buttons }
    };
}