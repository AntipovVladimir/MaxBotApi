using System.Text.Json.Serialization;
using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class InviteUserRequest: RequestBase<ApiResponse>
{
    public InviteUserRequest(long chatId) : base(string.Format("chats/{0}/members", chatId))
    {
        HttpMethod = HttpMethod.Post;
    }
    
    /// <summary>
    /// Массив ID пользователей для добавления в чат
    /// </summary>
    [JsonPropertyName("user_ids")]
    public required IEnumerable<long> UserIds { get; set; }
}