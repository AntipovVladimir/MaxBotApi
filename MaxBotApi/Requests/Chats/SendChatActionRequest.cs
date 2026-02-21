using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class SendChatActionRequest : RequestBase<ApiResponse>
{
    public SendChatActionRequest(long chat_id) : base(string.Format("chats/{0}/actions", chat_id))
    {
        HttpMethod = HttpMethod.Post;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonPropertyName("action")]
    public SenderAction Action { get; set; }
}