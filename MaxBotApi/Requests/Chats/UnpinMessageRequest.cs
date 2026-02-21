using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class UnpinMessageRequest : RequestBase<ApiResponse>
{
    public UnpinMessageRequest(long chatId) : base(string.Format("chats/{0}/pin", chatId))
    {
        HttpMethod = HttpMethod.Delete;
    }
}