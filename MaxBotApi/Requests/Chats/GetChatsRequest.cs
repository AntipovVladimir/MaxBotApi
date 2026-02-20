using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetChatsRequest : ParameterlessRequest<ChatsResponse>
{
    public GetChatsRequest() : base("chats")
    {
        HttpMethod = HttpMethod.Get;
    }

    public GetChatsRequest(int count = 50, long? marker = null)
        : base(marker is null
            ? string.Format("chats?count={0}", count)
            : string.Format("chats?count={0}&marker={1}", count, marker))
    {
        HttpMethod = HttpMethod.Get;
    }
}