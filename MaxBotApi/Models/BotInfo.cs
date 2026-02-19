using System.Text;
using System.Text.Json.Serialization;

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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(base.ToString());
        if (Description != null)
        {
            sb.Append(", Description:");
            sb.Append(Description);
        }

        if (AvatarUrl != null)
        {
            sb.Append(", AvatarUrl:");
            sb.Append(AvatarUrl);
        }

        if (FullAvatarUrl != null)
        {
            sb.Append(", FullAvatarUrl");
            sb.Append(FullAvatarUrl);
        }

        if (Commands != null)
        {
            sb.AppendLine(", Commands:");
            foreach (BotCommand command in Commands)
                sb.AppendLine(command.ToString());

            sb.AppendLine("===");
        }
        return sb.ToString();
    }
}