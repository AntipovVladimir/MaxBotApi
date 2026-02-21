using MaxBotApi.Types;

namespace MaxBotApi;

public class FileRequestBase<TResponse>(string methodName) : RequestBase<TResponse>(methodName)
{
    public required InputFileStream FileStream { get; set; }

    public override HttpContent? ToHttpContent()
    {
        var multipartContent = new MultipartFormDataContent();
        var mediaPartContent = new StreamContent(FileStream.Content);
        multipartContent.Add(mediaPartContent, "data", FileStream.FileName);
        return multipartContent;
    }
}