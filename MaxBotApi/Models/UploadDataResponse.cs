using System.Text.Json.Serialization;
using MaxBotApi.Extensions;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Models;

public class UploadDataResponse
{
    [JsonPropertyName("token")] public string? Token { get; set; }

    [JsonPropertyName("retval")] public int? Retval { get; set; }

    [JsonPropertyName("error_code")] public int? ErrorCode { get; set; }

    [JsonPropertyName("error_data")] public string? ErrorData { get; set; }

    /// <summary>
    /// Токены, полученные после загрузки изображений, только для UploadType=Image
    /// </summary>
    [JsonPropertyName("photos")]
    public Dictionary<string, UploadedInfo>? Photos { get; set; }

    public static explicit operator FileAttachmentRequest(UploadDataResponse response) => new FileAttachmentRequest()
        { Payload = new UploadedInfo() { Token = response.Token ?? throw new ArgumentNullException(nameof(response.Token)) } };

    public static explicit operator VideoAttachmentRequest(UploadDataResponse response) => new VideoAttachmentRequest()
        { Payload = new UploadedInfo() { Token = response.Token ?? throw new ArgumentNullException(nameof(response.Token)) } };

    public static explicit operator AudioAttachmentRequest(UploadDataResponse response) => new AudioAttachmentRequest()
        { Payload = new UploadedInfo() { Token = response.Token ?? throw new ArgumentNullException(nameof(response.Token)) } };

    public static explicit operator ImageAttachmentRequest(UploadDataResponse response) =>
        new() { Payload = new PhotoAttachmentRequestPayload() { Photos = response.Photos } };


    public override string ToString() => this.SerializeToString();
}