using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

/// <summary>Ответ от API на запрос PATCH me/commands</summary>
public class CommandsResponse
{
    /// <summary>
    /// до 32 элементов
    /// Команды, поддерживаемые ботом
    /// </summary>
    [JsonPropertyName("commands")]
    public BotCommand[]? Commands { get; set; }
}