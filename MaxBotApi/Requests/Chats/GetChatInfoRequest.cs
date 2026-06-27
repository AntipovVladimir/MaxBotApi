using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatInfoRequest :ParameterlessRequest<Chat>
{
    public GetChatInfoRequest(long chatId) : base(string.Format("chats/{0}", chatId))
    {
        HttpMethod = HttpMethod.Get;
    }

    public GetChatInfoRequest(string chatLink) : base(string.Format("chats/{0}", chatLink))
    {
        HttpMethod = HttpMethod.Get;
    }
}