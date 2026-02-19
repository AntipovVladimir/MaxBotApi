using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetVideoInfoRequest : RequestBase<VideoInfo>
{
    public GetVideoInfoRequest(string video_token) : base(string.Format("videos/{0}", video_token))
    {
        HttpMethod = HttpMethod.Get;
    }
}