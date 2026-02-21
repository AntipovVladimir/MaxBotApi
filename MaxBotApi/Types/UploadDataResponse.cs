using System.Text.Json.Serialization;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Types;

public class UploadDataResponse
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("retval")]
    public int? Retval { get; set; }

    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; set; }

    [JsonPropertyName("error_data")]
    public string? ErrorData { get; set; }
    
    /// <summary>
    /// Токены, полученные после загрузки изображений, только для UploadType=Image
    /// </summary>
    [JsonPropertyName("photos")]
    public Dictionary<string, UploadedInfo>? Photos { get; set; }
}