using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetWebhookInfoRequest : ParameterlessRequest<Subscriptions>
{
    public GetWebhookInfoRequest() : base("subscriptions")
    {
        HttpMethod = HttpMethod.Get;
    }
} 