using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class BotInfo : User
{
    /// <summary>
    /// до 16000 символов
    /// Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// URL аватара пользователя или бота в уменьшенном размере
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// URL аватара пользователя или бота в полном размере
    /// </summary>
    [JsonPropertyName("full_avatar_url")]
    public string? FullAvatarUrl { get; set; }

    /// <summary>
    /// до 32 элементов
    /// Команды, поддерживаемые ботом
    /// </summary>
    [JsonPropertyName("commands")]
    public BotCommand[]? Commands { get; set; }

    public override string ToString() => this.SerializeToString();
}