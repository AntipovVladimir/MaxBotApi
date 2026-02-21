using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MaxBotApi.Serialization;
using MaxBotApi.Types;

namespace MaxBotApi;

public class FileRequestBase<TResponse>(string methodName) : RequestBase<TResponse>(methodName)
{
    [JsonIgnore]
    public required string FileName { get; set; }
    
    [JsonIgnore]
    public string? Token { get; set; }

    private static readonly Encoding Latin1 = Encoding.GetEncoding(28591); 
    public override HttpContent? ToHttpContent()
    {
        return null;
    }
}