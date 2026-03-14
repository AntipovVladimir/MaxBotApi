using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Extensions;

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

    public override string ToString() => this.SerializeToString();
}