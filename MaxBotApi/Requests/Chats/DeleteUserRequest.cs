using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class KickUserRequest : RequestBase<ApiResponse>
{
    public KickUserRequest(long chatId, long userId) : base(string.Format("chats/{0}/members?user_id={1}", chatId, userId))
    {
        HttpMethod = HttpMethod.Delete;
    }
}

public class KickBanRequest : RequestBase<ApiResponse>
{
    public KickBanRequest(long chatId, long userId) : base(string.Format("chats/{0}/members?user_id={1}&block=true", chatId, userId))
    {
        HttpMethod = HttpMethod.Delete;
    }
}