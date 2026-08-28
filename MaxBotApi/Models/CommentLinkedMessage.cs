using System.Text.Json.Serialization;
using MaxBotApi.Enums;

namespace MaxBotApi.Models;

public class CommentLinkedMessage
{
    /// <summary>
    /// Для комментариев поддерживается только тип reply
    /// </summary>
    [JsonPropertyName("type")]
    public MessageLinkType Type { get; set; }
    
    /// <summary>
    /// Пользователь или бот, отправивший комментарий
    /// </summary>
    [JsonPropertyName("sender")]
    public User? Sender { get; set; }
    
    /// <summary>
    /// Чат или канал, в котором сообщение было изначально опубликовано. Только для пересланных сообщений с type = forward.
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long? ChatId { get; set; }

    /// <summary>
    /// Информация о комментарии
    /// </summary>
    [JsonPropertyName("message")] 
    public required CommentMessageBody Message { get; set; }
    
}