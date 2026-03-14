using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class UploadUrlResponse
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    public override string ToString() => this.SerializeToString();
}