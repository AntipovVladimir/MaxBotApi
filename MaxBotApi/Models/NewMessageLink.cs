using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class NewMessageLink
{
    /// <summary>
    /// Тип ссылки сообщения
    /// </summary>
    [JsonPropertyName("type")]
    public required MessageLinkType Type { get; set; }
    
    /// <summary>
    /// ID сообщения исходного сообщения
    /// </summary>
    [JsonPropertyName("mid")]
    public required string MessageId { get; set; }
}