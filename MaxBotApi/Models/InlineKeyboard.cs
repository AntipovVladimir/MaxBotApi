using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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

    public static explicit operator InlineKeyboardAttachment(InlineKeyboard ik) => new InlineKeyboardAttachment() { Payload = ik };
    
}