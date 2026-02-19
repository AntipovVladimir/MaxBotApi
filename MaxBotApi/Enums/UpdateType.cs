using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]

public enum UpdateType
{
    [JsonStringEnumMemberName("message_created")]
    MessageCreated,
    [JsonStringEnumMemberName("message_callback")]
    MessageCallback,
    [JsonStringEnumMemberName("message_edited")]
    MessageEdited,
    [JsonStringEnumMemberName("message_removed")]
    MessageRemoved,
    [JsonStringEnumMemberName("bot_added")]
    BotAdded,
    [JsonStringEnumMemberName("bot_removed")]
    BotRemoved,
    [JsonStringEnumMemberName("dialog_muted")]
    DialogMuted,
    [JsonStringEnumMemberName("dialog_unmuted")]
    DialogUnmuted,
    [JsonStringEnumMemberName("dialog_cleared")]
    DialogCleared,
    [JsonStringEnumMemberName("dialog_removed")]
    DialogRemoved,
    [JsonStringEnumMemberName("user_added")]
    UserAdded,
    [JsonStringEnumMemberName("user_removed")]
    UserRemoved,
    [JsonStringEnumMemberName("bot_started")]
    BotStarted,
    [JsonStringEnumMemberName("bot_stopped")]
    BotStopped,
    [JsonStringEnumMemberName("chat_title_changed")]
    ChatTitleChanged
}