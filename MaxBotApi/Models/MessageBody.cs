using System.Text;
using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class MessageBody
{
    /// <summary>
    /// Уникальный ID сообщения
    /// </summary>
    [JsonPropertyName("mid")]
    public required string MessageId { get; set; }

    /// <summary>
    /// ID последовательности сообщения в чате
    /// </summary>
    [JsonPropertyName("seq")]
    public long SequenceId { get; set; }

    /// <summary>
    /// Новый текст сообщения
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Вложения сообщения. Могут быть одним из типов Attachment. Смотрите описание схемы
    /// </summary>
    [JsonPropertyName("attachments")]
    public Attachment[]? Attachments { get; set; }

    /// <summary>
    /// Разметка текста сообщения. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0
    /// </summary>
    [JsonPropertyName("markup")]
    public MarkupElement[]? Markup { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Message Id: ");
        sb.Append(MessageId);
        sb.Append(", SequenceId: ");
        sb.Append(SequenceId);
        if (Text != null)
        {
            sb.Append(", Text: ");
            sb.Append(Text);
        }

        if (Attachments?.Length > 0)
        {
            sb.AppendLine();
            sb.AppendLine(" Attachments: ");
            for (int i = 0; i < Attachments.Length; i++)
            {
                sb.Append("[");
                sb.Append(Attachments[i]);
                sb.AppendLine("]");
            }
            sb.AppendLine("---");
        }

        if (Markup?.Length > 0)
        {
            sb.AppendLine();
            sb.AppendLine(" Markup: ");
            for (int i = 0; i < Markup.Length; i++)
            {
                sb.Append("[");
                sb.Append(Markup[i]);
                sb.AppendLine("]");
            }
            sb.AppendLine("---");
        }

        return sb.ToString();
    }
}