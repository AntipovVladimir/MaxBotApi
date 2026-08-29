using System.Text.Json.Serialization;
using MaxBotApi.Extensions;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class CommentMessage
{
    /// <summary>
    /// Пользователь, отправивший комментарий. Может быть null, если сообщение было опубликовано от имени канала
    /// </summary>
    [JsonPropertyName("sender")]
    public User? Sender { get; set; }
    
    /// <summary>
    /// Получатель сообщения: для комментариев — канал
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
    /// Комментарий, на который получен ответ
    /// </summary>
    [JsonPropertyName("link")]
    public CommentLinkedMessage? Link { get; set; }
    
    /// <summary>
    /// Информация о комментарии
    /// </summary>
    [JsonPropertyName("body")]
    public required CommentMessageBody Body { get; set; }
    
    public override string ToString() => this.SerializeToString();
}