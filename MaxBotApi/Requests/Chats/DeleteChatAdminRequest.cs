using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class DeleteChatAdminRequest : RequestBase<ApiResponse>
{
    public DeleteChatAdminRequest(long chat_id, long user_id) : base(string.Format("chats/{0}/members/admins/{1}", chat_id, user_id))
    {
        HttpMethod = HttpMethod.Delete;
    }
}