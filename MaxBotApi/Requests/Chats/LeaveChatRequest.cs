using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class LeaveChatRequest : RequestBase<ApiResponse>
{
    public LeaveChatRequest(long chatId) : base(string.Format("chats/{0}/members/me", chatId))
    {
        HttpMethod = HttpMethod.Delete;
    }
}