using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class BotCommand
{
    /// <summary>
    /// от 1 до 64 символов, Название команды
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// от 1 до 128 символов, Описание команды (по желанию)
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    public override string ToString() => this.SerializeToString();
}