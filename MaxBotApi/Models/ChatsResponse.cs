using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

/// <summary>
/// Список запрашиваемых чатов
/// </summary>
public class ChatsResponse
{
    [JsonPropertyName("chats")]
    public required IEnumerable<Chat> Chats { get; set; }
    public override string ToString() => this.SerializeToString();
}