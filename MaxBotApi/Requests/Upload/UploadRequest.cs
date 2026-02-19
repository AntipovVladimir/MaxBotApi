using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests.Upload;

public class UploadRequest : RequestBase<UrlResponse>
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
    

    
    /*using var formContent = new MultipartFormDataContent("NKdKd9Yk");
formContent.Headers.ContentType.MediaType = "multipart/form-data";
using var stream = file.OpenReadStream();
formContent.Add(new StreamContent(stream), "file", fileName);
using var response = await httpClient.PostAsync(GetDocumentUpdateRelativeUrl(), formContent);
*/
}