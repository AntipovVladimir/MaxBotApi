using System.Text.Json.Serialization;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Types;

public class UploadUrlResponse
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    
    [JsonPropertyName("token")]
    public string? Token { get; set; }
   
}