using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class DeleteChatRequest : RequestBase<ApiResponse>
{
    public DeleteChatRequest(long chat_id) : base(string.Format("chats/{0}", chat_id))
    {
        HttpMethod = HttpMethod.Delete;
    }
}