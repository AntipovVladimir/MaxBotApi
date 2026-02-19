using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatMembersRequest : RequestBase<ChatMembersResponse>
{
    public GetChatMembersRequest(long chat_id) : base(string.Format("chats/{0}/members", chat_id))
    {
        HttpMethod = HttpMethod.Get;
    }

    public GetChatMembersRequest(long chat_id, long marker, int count = 20) : base(string.Format("chats/{0}/members?marker={1}&count={2}", chat_id, marker, count))
    {
        HttpMethod = HttpMethod.Get;
    }
}