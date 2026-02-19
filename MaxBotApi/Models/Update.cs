using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(PolymorphicJsonConverter<Update>))]
[CustomJsonPolymorphic("update_type")]
[CustomJsonDerivedType(typeof(MessageCreatedUpdate), "message_created")]
[CustomJsonDerivedType(typeof(MessageCallbackUpdate), "message_callback")]
[CustomJsonDerivedType(typeof(MessageEditedUpdate), "message_edited")]
[CustomJsonDerivedType(typeof(MessageRemovedUpdate), "message_removed")]
[CustomJsonDerivedType(typeof(BotAddedUpdate), "bot_added")]
[CustomJsonDerivedType(typeof(BotRemovedUpdate), "bot_removed")]
[CustomJsonDerivedType(typeof(DialogMutedUpdate), "dialog_muted")]
[CustomJsonDerivedType(typeof(DialogUnmutedUpdate), "dialog_unmuted")]
[CustomJsonDerivedType(typeof(DialogClearedUpdate), "dialog_cleared")]
[CustomJsonDerivedType(typeof(DialogRemovedUpdate), "dialog_removed")]
[CustomJsonDerivedType(typeof(BotStartedUpdate), "bot_started")]
[CustomJsonDerivedType(typeof(BotStoppedUpdate), "bot_stopped")]
[CustomJsonDerivedType(typeof(ChatTitleChangedUpdate), "chat_title_changed")]
public abstract class Update
{
    /// <summary>
    /// ОбъектUpdate представляет различные типы событий, произошедших в чате. См. его наследников
    /// </summary>
    [JsonPropertyName("update_type")]
   
    public  UpdateType UpdateType { get; set; }

    /// <summary>
    /// Unix-время, когда произошло событие
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime TimeStamp { get; set; }


    [JsonIgnore] public static readonly UpdateType[] AllTypes =
    [
        UpdateType.MessageCreated,
        UpdateType.MessageCallback,
        UpdateType.MessageEdited,
        UpdateType.MessageRemoved,
        UpdateType.BotAdded,
        UpdateType.BotRemoved,
        UpdateType.DialogMuted,
        UpdateType.DialogUnmuted,
        UpdateType.DialogCleared,
        UpdateType.DialogRemoved,
        UpdateType.UserAdded,
        UpdateType.UserRemoved,
        UpdateType.BotStarted,
        UpdateType.BotStopped,
        UpdateType.ChatTitleChanged
    ];

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        return sb.ToString();
    }
}

public class MessageCreatedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.MessageCreated;

    /// <summary>
    /// Новое созданное сообщение
    /// </summary>
    [JsonPropertyName("message")]
    public required Message Message { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", Message: ");
        sb.Append(Message);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class MessageCallbackUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.MessageCallback;

    /// <summary>
    /// клавиатура
    /// </summary>
    [JsonPropertyName("callback")]
    public required Callback Callback { get; set; }

    /// <summary>
    /// Изначальное сообщение, содержащее встроенную клавиатуру. Может быть null, если оно было удалено к моменту, когда бот получил это обновление
    /// </summary>
    [JsonPropertyName("message")]
    public Message? Message { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        if (Message != null)
        {
            sb.Append(", Message: ");
            sb.Append(Message);
        }

        sb.Append(", Callback: ");
        sb.Append(Callback);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class MessageEditedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.MessageEdited;

    /// <summary>
    /// Отредактированное сообщение
    /// </summary>
    [JsonPropertyName("message")]
    public required Message Message { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", EditedMessage: ");
        sb.Append(Message);
        return sb.ToString();
    }
}

public class MessageRemovedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.MessageRemoved;

    /// <summary>
    /// ID удаленного сообщения
    /// </summary>
    [JsonPropertyName("message_id")]
    public required string MessageId { get; set; }

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, удаливший сообщение
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", UserId: ");
        sb.Append(UserId);
        return sb.ToString();
    }
}

public class BotAddedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.BotAdded;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, добавивший бота в чат
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Указывает, был ли бот добавлен в канал или нет
    /// </summary>
    [JsonPropertyName("is_channel")]
    public bool IsChannel { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", User: ");
        sb.Append(User);
        sb.Append(", IsChannel: ");
        sb.Append(IsChannel);
        return sb.ToString();
    }
}

public class BotRemovedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.BotRemoved;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, удаливший бота из чата
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Указывает, был ли бот удалён из канала или нет
    /// </summary>
    [JsonPropertyName("is_channel")]
    public bool IsChannel { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", User: ");
        sb.Append(User);
        sb.Append(", IsChannel: ");
        sb.Append(IsChannel);
        return sb.ToString();
    }
}

public class DialogMutedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.DialogMuted;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, который включил уведомления
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Время в формате Unix, до наступления которого диалог был отключён
    /// </summary>
    [JsonPropertyName("muted_until")]
    public long MutedUntil { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        sb.Append(", MutedUntil: ");
        sb.Append(MutedUntil);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class DialogUnmutedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.DialogUnmuted;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, который включил уведомления
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class DialogClearedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.DialogCleared;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, который включил уведомления
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class DialogRemovedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.DialogRemoved;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, который удалил чат
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class UserAddedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.UserAdded;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, добавленный в чат
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Пользователь, который добавил пользователя в чат. Может быть null, если пользователь присоединился к чату по ссылке
    /// </summary>
    [JsonPropertyName("inviter_id")]
    public long? InviterId { get; set; }

    /// <summary>
    /// Указывает, был ли пользователь добавлен в канал или нет
    /// </summary>
    [JsonPropertyName("is_channel")]
    public bool IsChannel { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (InviterId != null)
        {
            sb.Append(", InviterId: ");
            sb.Append(InviterId);
        }

        sb.Append(", IsChannel: ");
        sb.Append(IsChannel);
        return sb.ToString();
    }
}

public class UserRemovedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.UserRemoved;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, удалённый из чата
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Администратор, который удалил пользователя из чата. Может быть null, если пользователь покинул чат сам
    /// </summary>
    [JsonPropertyName("admin_id")]
    public long? AdminId { get; set; }

    /// <summary>
    /// Указывает, был ли пользователь удалён из канала или нет
    /// </summary>
    [JsonPropertyName("is_channel")]
    public bool IsChannel { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (AdminId != null)
        {
            sb.Append(", AdminId: ");
            sb.Append(AdminId);
        }

        sb.Append(", IsChannel: ");
        sb.Append(IsChannel);
        return sb.ToString();
    }
}

public class BotStartedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.BotStarted;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, добавивший бота в чат
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Дополнительные данные из дип-линков, переданные при запуске бота
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (Payload != null)
        {
            sb.Append(", Payload: ");
            sb.Append(Payload);
        }

        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class BotStoppedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.BotStopped;

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, добавивший бота в чат
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    /// <summary>
    /// Текущий язык пользователя в формате IETF BCP 47
    /// </summary>
    [JsonPropertyName("user_locale")]
    public string? UserLocale { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (UserLocale != null)
        {
            sb.Append(", UserLocale: ");
            sb.Append(UserLocale);
        }

        return sb.ToString();
    }
}

public class ChatTitleChangedUpdate : Update
{
    //public override UpdateType UpdateType => UpdateType.ChatTitleChanged;

    /// <summary>
    /// Новое название
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// ID чата, где произошло событие
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пользователь, который изменил название
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("UpdateType: ");
        sb.Append(UpdateType);
        sb.Append(", TimeStamp: ");
        sb.Append(TimeStamp);
        sb.Append(", ChatId: ");
        sb.Append(ChatId);
        sb.Append(", User: ");
        sb.Append(User);
        if (Title != null)
        {
            sb.Append(", Title: ");
            sb.Append(Title);
        }

        return sb.ToString();
    }
}