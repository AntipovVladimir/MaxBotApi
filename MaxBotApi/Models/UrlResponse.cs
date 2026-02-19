using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class UrlResponse
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}