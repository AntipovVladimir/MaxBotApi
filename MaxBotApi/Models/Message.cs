using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class Message
{
    /// <summary>
    /// Пользователь, отправивший сообщение
    /// </summary>
    [JsonPropertyName("sender")]
    public required User Sender { get; set; }
    
    /// <summary>
    /// Получатель сообщения. Может быть пользователем или чатом
    /// </summary>
    [JsonPropertyName("recipient")]
    public required Recipient Recipient { get; set; }
    
    /// <summary>
    /// Время создания сообщения в формате Unix-time
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime TimeStamp { get; set; }
    
    /// <summary>
    /// Пересланное или ответное сообщение
    /// </summary>
    [JsonPropertyName("link")]
    public LinkedMessage? Link { get; set; }
    
    /// <summary>
    /// Содержимое сообщения. Текст + вложения. Может быть null, если сообщение содержит только пересланное сообщение
    /// </summary>
    [JsonPropertyName("body")]
    public MessageBody? MessageBody { get; set; }
    
    /// <summary>
    /// Статистика сообщения.
    /// </summary>
    [JsonPropertyName("stat")]
    public MessageStat? Stat { get; set; }
    
    /// <summary>
    /// Публичная ссылка на пост в канале. Отсутствует для диалогов и групповых чатов
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Sender: ");
        sb.Append(Sender);
        sb.Append(", Recipient: ");
        sb.Append(Recipient);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        if (Link != null)
        {
            sb.Append(", Link: ");
            sb.Append(Link);
        }

        if (MessageBody != null)
        {
            sb.Append(", Body: ");
            sb.Append(MessageBody);
        }

        if (Stat != null)
        {
            sb.Append(", Stat: ");
            sb.Append(Stat);
        }

        if (Url != null)
        {
            sb.Append(", Url: ");
            sb.Append(Url);
        }

        return sb.ToString();
    }
}