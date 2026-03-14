using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class MessagesResponse
{
    /// <summary>
    /// Массив сообщений
    /// </summary>
    [JsonPropertyName("messages")]
    public required Message[]  Messages { get; set; }
    public override string ToString() => this.SerializeToString();
}