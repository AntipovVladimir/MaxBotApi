using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class FailedUserDetails
{
    /// <summary>
    /// Код ошибки
    /// Возможные значения:
    /// add.participant.privacy — ошибки конфиденциальности при добавлении пользователей
    /// add.participant.not.found — пользователи не найдены
    /// </summary>
    [JsonPropertyName("error_code")]
    public required string ErrorCode { get; set; }

    /// <summary>
    /// ID пользователей с данной ошибкой
    /// </summary>
    [JsonPropertyName("user_ids")]
    public required IEnumerable<long> UserIds { get; set; }
}