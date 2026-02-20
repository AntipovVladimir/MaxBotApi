using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class Chat
{
    /// <summary>
    /// ID чата
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Тип чата: dialog, chat, channel
    /// </summary>
    [JsonPropertyName("type")]
    public ChatType Type { get; set; }

    /// <summary>
    /// Enum: "active" "removed" "left" "closed"
    /// Статус чата:
    /// "active" — Бот является активным участником чата.
    /// "removed" — Бот был удалён из чата.
    /// "left" — Бот покинул чат.
    /// "closed" — Чат был закрыт.
    /// </summary>
    [JsonPropertyName("status")]
    public ChatStatus Status { get; set; }
    
    /// <summary>
    /// Отображаемое название чата. Может быть null для диалогов
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    /// <summary>
    /// Время последнего события в чате
    /// </summary>
    [JsonPropertyName("last_event_time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime LastEventTime { get; set; }
    
    /// <summary>
    /// Количество участников чата. Для диалогов всегда 2
    /// </summary>
    [JsonPropertyName("participants_count")]
    public int ParticipantsCount { get; set; }
    
    /// <summary>
    /// ID владельца чата
    /// </summary>
    [JsonPropertyName("owner_id")]
    public long? OwnerId { get; set; }
    
    /*
     Эта часть API у MAX сломана, 
     объект вида "participants":{"182995925":1771354246225,"4922171":1771359007345}
     массивом не является 
     /// <summary>
    /// Участники чата с временем последней активности. Может быть null, если запрашивается список чатов
    /// </summary>
    //[JsonPropertyName("participants")]
    //public ...? Participants { get; set; }
    */
    /// <summary>
    /// Доступен ли чат публично (для диалогов всегда false)
    /// </summary>
    [JsonPropertyName("is_public")]
    public bool IsPublic { get; set; }
    
    /// <summary>
    /// Ссылка на чат
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }
    
    /// <summary>
    /// Описание чата
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    
    /// <summary>
    /// Данные о пользователе в диалоге (только для чатов типа "dialog")
    /// </summary>
    [JsonPropertyName("dialog_with_user")]
    public UserWithPhoto? DialogWithUser { get; set; }
    
    /// <summary>
    /// ID сообщения, содержащего кнопку, через которую был инициирован чат
    /// </summary>
    [JsonPropertyName("chat_message_id")]
    public string? ChatMessageId { get; set; }
    
    /// <summary>
    /// Закреплённое сообщение в чате (возвращается только при запросе конкретного чата)
    /// </summary>
    [JsonPropertyName("pinned_message")]
    public Message? PinnedMessage { get; set; }
}