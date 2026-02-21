using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;

namespace MaxBotApi.Models;

public class ChatAdmin
{
    /// <summary>
    /// Идентификатор пользователя-участника чата, который назначается администратором
    /// Максимум — 50 администраторов в чате
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserID { get; set; }

    /// <summary>
    /// Перечень прав доступа пользователя. Возможные значения:
    /// "read_all_messages" — Читать все сообщения. Это право важно при назначении ботов: без него бот не будет получать апдейты (вебхуки) в групповом чате
    /// "add_remove_members" — Добавлять/удалять участников
    /// "add_admins" — Добавлять администраторов
    /// "change_chat_info" — Изменять информацию о чате
    /// "pin_message" — Закреплять сообщения
    /// "write" — Писать сообщения
    /// "can_call" — Совершать звонки
    /// "edit_link" — Изменять ссылку на чат
    /// "post_edit_delete_message" — Публиковать, редактировать и удалять сообщения
    /// "edit_message" — Редактировать сообщения
    /// "delete_message" — Удалять сообщения
    /// Если право назначается администратору, то обновляются его текущие права доступа
    /// </summary>
    [JsonPropertyName("permissions")]
    public required IEnumerable<ChatAdminPermission> Permissions { get; set; }

    /// <summary>
    /// Титул администратора (вместо "админ" и "владелец")
    /// </summary>
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("[ChatAdmin] UserID: ");
        sb.Append(UserID);
        sb.Append(", Permissions: ");
        sb.Append(string.Join(", ", Permissions));
        sb.Append("; Alias: ");
        sb.Append(Alias);
        return sb.ToString();
    }
}