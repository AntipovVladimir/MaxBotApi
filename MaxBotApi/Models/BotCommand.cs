using System.Text.Json.Serialization;

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

    public override string ToString()
    {
        return string.Format("Name: {0}, Description : {1}", Name, Description);
    }
}