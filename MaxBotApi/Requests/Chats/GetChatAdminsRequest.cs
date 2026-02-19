using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatAdminsRequest :ParameterlessRequest<ChatMembersResponse>
{
    public GetChatAdminsRequest(long chatId) : base(string.Format("chats/{0}/members/admins", chatId))
    {
        HttpMethod = HttpMethod.Get;
    }
}