using System.Text;
using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class LinkedMessage
{
    /// <summary>
    /// Тип связанного сообщения
    /// </summary>
    [JsonPropertyName("type")]
    public MessageLinkType Type { get; set; }
    
    /// <summary>
    /// Пользователь, отправивший сообщение.
    /// </summary>
    [JsonPropertyName("sender")]
    public User? Sender { get; set; }
    
    /// <summary>
    /// Чат, в котором сообщение было изначально опубликовано. Только для пересланных сообщений
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long? ChatId { get; set; }
    
    /// <summary>
    /// Схема, представляющая тело сообщения
    /// </summary>
    [JsonPropertyName("message")]
    public required MessageBody MessageBody { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Type: ");
        sb.Append(Type);
        sb.Append(", Sender: ");
        sb.Append(Sender);
        sb.Append(", Chat Id: ");
        sb.Append(ChatId);
        sb.Append(", Text: ");
        sb.Append(MessageBody);
        return sb.ToString();
    }
}