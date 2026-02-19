using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class ChatMembersResponse
{
    /// <summary>
    /// Список участников чата с информацией о времени последней активности
    /// </summary>
    [JsonPropertyName("members")]
    public required ChatMember[] ChatMembers { get; set; }
    
    /// <summary>
    /// Указатель на следующую страницу данных
    /// </summary>
    [JsonPropertyName("marker")]
    public long? Marker  { get; set; }
    
}