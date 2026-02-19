using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetMeRequest : ParameterlessRequest<BotInfo>
{
    public GetMeRequest() : base("me")
    {
        HttpMethod = HttpMethod.Get;
    }
}