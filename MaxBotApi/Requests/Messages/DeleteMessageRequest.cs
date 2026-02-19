using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class DeleteMessageRequest : RequestBase<ApiResponse>
{
    public DeleteMessageRequest(string message_id) : base(string.Format("messages?message_id={0}", message_id))
    {
        HttpMethod = HttpMethod.Delete;
    }
}