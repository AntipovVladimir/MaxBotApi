using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatAdminPermission
{
    [JsonStringEnumMemberName("read_all_messages")]
    ReadAllMessages,
    [JsonStringEnumMemberName("add_remove_members")]
    AddRemoveMembers,
    [JsonStringEnumMemberName("add_admins")]
    AddAdmins,
    [JsonStringEnumMemberName("change_chat_info")]
    ChangeChatInfo,
    [JsonStringEnumMemberName("pin_message")]
    PinMessage,
    [JsonStringEnumMemberName("write")]
    Write,
    [JsonStringEnumMemberName("can_call")]
    CanCall,
    [JsonStringEnumMemberName("edit_link")]
    EditLink,
    [JsonStringEnumMemberName("post_edit_delete_message")]
    PostEditDeleteMessage,
    [JsonStringEnumMemberName("edit_message")]
    EditMessage,
    [JsonStringEnumMemberName("delete_message")]
    DeleteMessage,
    [JsonStringEnumMemberName("view_stats")]
    ViewStats,
    [JsonStringEnumMemberName("delete")]
    Delete,
    [JsonStringEnumMemberName("edit")]
    Edit
    
}