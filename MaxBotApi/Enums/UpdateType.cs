using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]

public enum UpdateType
{
    /// <summary>
    /// Новое сообщение
    /// </summary>
    [JsonStringEnumMemberName("message_created")]
    MessageCreated,
    /// <summary>
    /// Событие callback (нажата кнопка)
    /// </summary>
    [JsonStringEnumMemberName("message_callback")]
    MessageCallback,
    /// <summary>
    /// Сообщение отредактировано
    /// </summary>
    [JsonStringEnumMemberName("message_edited")]
    MessageEdited,
    /// <summary>
    /// Сообщение удалено
    /// </summary>
    [JsonStringEnumMemberName("message_removed")]
    MessageRemoved,
    /// <summary>
    /// Бот добавлен в группу
    /// </summary>
    [JsonStringEnumMemberName("bot_added")]
    BotAdded,
    /// <summary>
    /// Бот исключен из группы
    /// </summary>
    [JsonStringEnumMemberName("bot_removed")]
    BotRemoved,
    /// <summary>
    /// Уведомления в диалоге отключены
    /// </summary>
    [JsonStringEnumMemberName("dialog_muted")]
    DialogMuted,
    /// <summary>
    /// Уведомления в диалоге включены
    /// </summary>
    [JsonStringEnumMemberName("dialog_unmuted")]
    DialogUnmuted,
    /// <summary>
    /// Диалог очищен (все сообщения удалены)
    /// </summary>
    [JsonStringEnumMemberName("dialog_cleared")]
    DialogCleared,
    /// <summary>
    /// Диалог удален
    /// </summary>
    [JsonStringEnumMemberName("dialog_removed")]
    DialogRemoved,
    /// <summary>
    /// Пользователь добавлен в группу 
    /// </summary>
    [JsonStringEnumMemberName("user_added")]
    UserAdded,
    /// <summary>
    /// Пользователь удален из группы
    /// </summary>
    [JsonStringEnumMemberName("user_removed")]
    UserRemoved,
    /// <summary>
    /// Начат диалог с ботом
    /// </summary>
    [JsonStringEnumMemberName("bot_started")]
    BotStarted,
    /// <summary>
    /// Прекращен диалог с ботом
    /// </summary>
    [JsonStringEnumMemberName("bot_stopped")]
    BotStopped,
    /// <summary>
    /// Произошла смена названия группы
    /// </summary>
    [JsonStringEnumMemberName("chat_title_changed")]
    ChatTitleChanged
}