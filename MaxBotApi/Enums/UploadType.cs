using System.Net.Mime;
using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]

public enum UploadType
{
    /// <summary>
    /// image: JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
    /// </summary>
    [JsonStringEnumMemberName("image")]
    Image,
    /// <summary>
    /// video: MP4, MOV, MKV, WEBM, MATROSKA
    /// </summary>
    [JsonStringEnumMemberName("video")]
    Video,
    /// <summary>
    /// audio: MP3, WAV, M4A и другие
    /// </summary>
    [JsonStringEnumMemberName("audio")]
    Audio,
    /// <summary>
    /// file: любые типы файлов
    /// </summary>
    [JsonStringEnumMemberName("file")]
    File
}