using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatInfoRequest :ParameterlessRequest<Chat>
{
    public GetChatInfoRequest(long chatId) : base(string.Format("chats/{0}", chatId))
    {
        HttpMethod = HttpMethod.Get;
    }
}