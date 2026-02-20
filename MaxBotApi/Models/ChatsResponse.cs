using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

/// <summary>
/// Список запрашиваемых чатов
/// </summary>
public class ChatsResponse
{
    [JsonPropertyName("chats")]
    public required IEnumerable<Chat> Chats { get; set; }
}