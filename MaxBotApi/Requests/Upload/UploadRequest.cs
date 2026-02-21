using MaxBotApi.Enums;
using MaxBotApi.Types;

namespace MaxBotApi.Requests.Upload;

public class UploadRequest : RequestBase<UploadUrlResponse>
{
    public UploadRequest(UploadType uploadType) : base(string.Format("uploads?type={0}", uploadType switch
    {
        UploadType.Image => "image",
        UploadType.Video => "video",
        UploadType.Audio => "audio",
        _ => "file"
    }))
    {
        HttpMethod = HttpMethod.Post;
    }
}

public class UploadDataRequest : FileRequestBase<UploadDataResponse>
{
    public UploadDataRequest(string url) : base(url)
    {
        HttpMethod = HttpMethod.Post;
    }
}