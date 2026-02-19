using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatMyInfoRequest:RequestBase<ChatMember>
{
    public GetChatMyInfoRequest(long chat_id) : base(string.Format("chats/{0}/members/me", chat_id))
    {
        HttpMethod =HttpMethod.Get;
    }
}