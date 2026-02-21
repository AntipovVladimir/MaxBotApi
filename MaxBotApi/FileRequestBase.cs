using System.Text.Json.Serialization;

namespace MaxBotApi;

public class FileRequestBase<TResponse>(string methodName) : RequestBase<TResponse>(methodName)
{
    [JsonIgnore] public required string FileName { get; set; }

    [JsonIgnore] public string? Token { get; set; }

    public override HttpContent? ToHttpContent()
    {
        return null;
    }
}