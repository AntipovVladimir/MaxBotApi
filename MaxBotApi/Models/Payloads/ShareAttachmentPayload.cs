using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models.Payloads;

public class ShareAttachmentPayload
{
    /// <summary>
    /// URL, прикрепленный к сообщению в качестве предпросмотра медиа
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// Токен вложения
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    public override string ToString() => this.SerializeToString();
}