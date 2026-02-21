using System.Text.Json.Serialization;

namespace MaxBotApi.Types;

public class UploadDataResponse
{
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}