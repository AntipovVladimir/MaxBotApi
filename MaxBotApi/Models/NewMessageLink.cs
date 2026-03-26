using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class NewMessageLink
{
    /// <summary>
    /// Тип ссылки сообщения
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required MessageLinkType Type { get; set; }
    
    /// <summary>
    /// ID сообщения исходного сообщения
    /// </summary>
    [JsonPropertyName("mid")]
    public required string MessageId { get; set; }
    public override string ToString() => this.SerializeToString();
}