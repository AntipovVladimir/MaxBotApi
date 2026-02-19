using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class AddChatAdminsRequest:RequestBase<ApiResponse>
{
    public AddChatAdminsRequest(long chat_id) : base(string.Format("chats/{0}/members/admins", chat_id))
    {
        HttpMethod = HttpMethod.Post;
    }
    
    [JsonPropertyName("admins")]
    public required IEnumerable<ChatAdmin> Admins { get; set; }
    
}