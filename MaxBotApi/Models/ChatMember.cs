using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class ChatMember:User
{
    /// <summary>
    /// до 16000 символов
    /// Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// URL аватара пользователя или бота в уменьшенном размере
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// URL аватара пользователя или бота в полном размере
    /// </summary>
    [JsonPropertyName("full_avatar_url")]
    public string? FullAvatarUrl { get; set; }

    /// <summary>
    /// Время последней активности пользователя в чате. Может быть устаревшим для суперчатов (равно времени вступления)
    /// </summary>
    [JsonPropertyName("last_access_time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime LastAccessTime { get; set; }
    
    /// <summary>
    /// Является ли пользователь владельцем чата
    /// </summary>     
    [JsonPropertyName("is_owner")]
    public bool IsOwner { get; set; }
    
    /// <summary>
    /// Является ли пользователь администратором чата 
    /// </summary>
    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Дата присоединения к чату в формате Unix time
    /// </summary>
    [JsonPropertyName("join_time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime JoinTime { get; set; }
    
    /// <summary>
    /// Перечень прав пользователя. Возможные значения:
    /// "read_all_messages" — Читать все сообщения.
    /// "add_remove_members" — Добавлять/удалять участников.
    /// "add_admins" — Добавлять администраторов.
    /// "change_chat_info" — Изменять информацию о чате.
    /// "pin_message" — Закреплять сообщения.
    /// "write" — Писать сообщения.
    /// "edit_link" — Изменять ссылку на чат.
    /// </summary>
    
    [JsonPropertyName("permissions")]
    public ChatAdminPermission[]? Permissions { get; set; }
    
    /// <summary>
    /// Заголовок, который будет показан на клиенте
    /// Если пользователь администратор или владелец и ему не установлено это название, то поле не передаётся, клиенты на своей стороне подменят на "владелец" или "админ"
    /// </summary>
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }
    
}