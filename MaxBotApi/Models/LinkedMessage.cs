using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class LinkedMessage
{
    /// <summary>
    /// Тип связанного сообщения
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
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

    public override string ToString() => this.SerializeToString();
}