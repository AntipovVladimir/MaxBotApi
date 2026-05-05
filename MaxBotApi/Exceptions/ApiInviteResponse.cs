using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class ApiInviteResponse : ApiResponse
{
    /// <summary>
    /// ID пользователей, которых не удалось добавить
    /// </summary>
    [JsonPropertyName("failed_user_ids")]
    public IEnumerable<long>? FailedUserIds { get; set; }

    /// <summary>
    /// Подробное описание, почему пользователь не был добавлен в чат
    /// </summary>
    [JsonPropertyName("failed_user_details")]
    public IEnumerable<FailedUserDetails>? FailedUserDetails { get; set; }
}