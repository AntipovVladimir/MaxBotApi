using System.Text.Json.Serialization;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Models;

public class VideoInfo
{
    /// <summary>
    /// Токен видео-вложения
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// URL-ы для скачивания или воспроизведения видео. Может быть null, если видео недоступно
    /// </summary>
    [JsonPropertyName("urls")]
    public VideoUrls? Urls { get; set; }

    /// <summary>
    /// Миниатюра видео
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public PhotoAttachmentPayload? Thumbnail { get; set; }

    /// <summary>
    /// Ширина видео
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Высота видео
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Длина видео в секундах
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}