using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class VideoThumbnail
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}