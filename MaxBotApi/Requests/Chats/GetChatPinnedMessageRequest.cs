using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatPinnedMessageRequest:RequestBase<ApiMessage>
{
    public GetChatPinnedMessageRequest(long chat_id) : base(string.Format("chats/{0}/pin", chat_id))
    {
        HttpMethod = HttpMethod.Get;
    }
}