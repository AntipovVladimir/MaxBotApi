using System.Text.Json.Serialization;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class User : IEquatable<User>
{
    /// <summary>
    /// int64 - ID пользователя
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserID { get; set; }

    /// <summary>
    /// string - Отображаемое имя пользователя
    /// </summary>
    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; } 

    /// <summary>
    /// string [nullable] - Отображаемая фамилия пользователя
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// string [nullable] optional - deprecated
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// string - Уникальное публичное имя пользователя. Может быть null, если пользователь недоступен или имя не задано 
    /// </summary>
    [JsonPropertyName("username")]
    public string? UserName { get; set; }

    /// <summary>
    /// bool - true, если пользователь является ботом
    /// </summary>
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }

    /// <summary>
    /// int64 - Время последней активности пользователя в MAX (Unix-время в миллисекундах). Может быть неактуальным, если пользователь отключил статус "онлайн" в настройках.
    /// </summary>
    [JsonPropertyName("last_activity_time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime LastActivityTime { get; set; }


    public bool Equals(User? other)
    {
        return other is not null
               && other.UserID.Equals(UserID)
               && ((other.Name is null && Name is null) || (other.Name is not null && other.Name.Equals(Name, StringComparison.Ordinal)))
               && other.FirstName.Equals(FirstName, StringComparison.Ordinal)
               && ((other.LastName is null && LastName is null) || (other.LastName is not null && other.LastName.Equals(LastName, StringComparison.Ordinal)))
               && ((other.UserName is null && UserName is null) || (other.UserName is not null && other.UserName.Equals(UserName, StringComparison.Ordinal)))
               && other.IsBot == IsBot
               && other.LastActivityTime == LastActivityTime;
    }

    public override string ToString()
    {
        return string.Format("User: UserId: {0}, Name: {1}, FirstName: {2}, LastName: {3}, UserName: {4}, IsBot: {5}, LastActivityTime: {6}", UserID, Name,
            FirstName, LastName, UserName, IsBot, LastActivityTime);
    }
}