using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;

namespace MaxBotApi.Models;

public class MarkupElement
{
    /// <summary>
    /// Тип элемента разметки. Может быть жирный, курсив, зачеркнутый, подчеркнутый, моноширинный, ссылка или упоминание пользователя
    /// strong , emphasized, monospaced, link, strikethrough, underline, user_mention
    /// </summary>
    [JsonPropertyName("type")]
    public MarkupElementType Type { get; set; }
    
    /// <summary>
    /// Индекс начала элемента разметки в тексте. Нумерация с нуля
    /// </summary>
    [JsonPropertyName("from")]
    public int From { get; set; }
    
    /// <summary>
    /// Длина элемента разметки
    /// </summary>
    [JsonPropertyName("length")]
    public int Length { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Type: ");
        sb.Append(Type);
        sb.Append(", From: ");
        sb.Append(From);
        sb.Append(", Length: ");
        sb.Append(Length);
        return sb.ToString();
    }
}