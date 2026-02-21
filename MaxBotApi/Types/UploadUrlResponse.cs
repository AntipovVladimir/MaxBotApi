using System.Text.Json.Serialization;

namespace MaxBotApi.Types;

public class UploadUrlResponse
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}