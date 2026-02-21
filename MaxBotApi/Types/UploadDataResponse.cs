using System.Text.Json.Serialization;

namespace MaxBotApi.Types;

public class UploadDataResponse
{
    [JsonPropertyName("token")] public string? Token { get; set; }

    [JsonPropertyName("retval")] public int? Retval { get; set; }

    [JsonPropertyName("error_code")] public int? ErrorCode { get; set; }

    [JsonPropertyName("error_data")] public string? ErrorData { get; set; }
}