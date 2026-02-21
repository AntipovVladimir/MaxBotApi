using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class DeleteWebhookRequest : RequestBase<ApiResponse>
{
    public DeleteWebhookRequest(string hookurl) : base(string.Format("subscriptions?url={0}", hookurl))
    {
        HttpMethod = HttpMethod.Delete;
    }
}