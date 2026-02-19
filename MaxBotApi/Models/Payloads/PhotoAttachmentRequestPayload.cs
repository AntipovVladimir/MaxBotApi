using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class PhotoAttachmentRequestPayload
{
    /// <summary>
    /// Любой внешний URL изображения, которое вы хотите прикрепить
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    /// <summary>
    /// Токен существующего вложения
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    
    /// <summary>
    /// Токены, полученные после загрузки изображений
    /// </summary>
    [JsonPropertyName("photos")]
    public UploadedInfo? Photos { get; set; }
}
