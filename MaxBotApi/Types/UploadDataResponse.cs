using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace MaxBotApi.Types;

public class UploadDataResponse
{
    [XmlElement(ElementName = "token")]
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [XmlElement(ElementName = "retval")]
    [JsonPropertyName("retval")]
    public int? Retval { get; set; }

    [XmlElement(ElementName = "error_code")]
    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; set; }

    [XmlElement(ElementName = "error_data")]
    [JsonPropertyName("error_data")]
    public string? ErrorData { get; set; }
}