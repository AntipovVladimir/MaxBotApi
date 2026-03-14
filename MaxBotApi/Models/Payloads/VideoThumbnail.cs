using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models.Payloads;

public class VideoThumbnail
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    public override string ToString() => this.SerializeToString();
}