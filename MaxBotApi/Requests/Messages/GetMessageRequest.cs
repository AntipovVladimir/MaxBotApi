using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetMessageRequest : RequestBase<Message>
{
    public GetMessageRequest(string message_id) : base(string.Format("messages/{0}", message_id))
    {
        HttpMethod = HttpMethod.Get;
    }
}