using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatAdminsRequest : ParameterlessRequest<ChatMembersResponse>
{
    public GetChatAdminsRequest(long chatId, long? marker) : base((marker is null)
        ? string.Format("chats/{0}/members/admins", chatId)
        : string.Format("/chats/{0}/members/admins&marker={1}", chatId, marker))
    {
        HttpMethod = HttpMethod.Get;
    }
}