using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class VideoUrls
{
    /// <summary>
    /// URL видео в разрешении 1080p, если доступно
    /// </summary>
    [JsonPropertyName("mp4_1080")]
    public string? mp4_1080 { get; set; }
    
    /// <summary>
    /// URL видео в разрешении 720p, если доступно
    /// </summary>
    [JsonPropertyName("mp4_720")]
    public string? mp4_720 { get; set; }
    
    /// <summary>
    /// URL видео в разрешении 480p, если доступно 
    /// </summary>
    [JsonPropertyName("mp4_480")]
    public string? mp4_480 { get; set; }

    /// <summary>
    /// URL видео в разрешении 360p, если доступно 
    /// </summary>
    [JsonPropertyName("mp4_360")]
    public string? mp4_360 { get; set; }

    /// <summary>
    /// URL видео в разрешении 240p, если доступно 
    /// </summary>
    [JsonPropertyName("mp4_240")]
    public string? mp4_240 { get; set; }

    /// <summary>
    /// URL видео в разрешении 144p, если доступно 
    /// </summary>
    [JsonPropertyName("mp4_144")]
    public string? mp4_144 { get; set; }

    /// <summary>
    /// URL трансляции, если доступна
    /// </summary>
    [JsonPropertyName("hls")]
    public string? HLS { get; set; }
    
}