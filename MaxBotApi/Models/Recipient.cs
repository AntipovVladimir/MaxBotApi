using System.Text.Json.Serialization;
using MaxBotApi.Enums;

namespace MaxBotApi.Models;

public class Recipient
{
    /// <summary>
    /// ID чата
    /// </summary>
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    /// <summary>
    /// Тип чата
    /// </summary>
    [JsonPropertyName("chat_type")]
    public required ChatType ChatType { get; set; }

    /// <summary>
    /// ID пользователя, если сообщение было отправлено пользователю
    /// </summary>
    [JsonPropertyName("user_id")]
    public long? UserId { get; set; }

    public override string ToString()
    {
        return string.Format("Recepient: ChatId:{0}, ChatType:{1}, UserId:{2}", ChatId, ChatType, UserId);
    }
}