using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class MessagesResponse
{
    /// <summary>
    /// Массив сообщений
    /// </summary>
    [JsonPropertyName("messages")]
    public required Message[]  Messages { get; set; }
}