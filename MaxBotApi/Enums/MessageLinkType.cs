using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageLinkType
{
    /// <summary>
    /// Пересланное сообщение
    /// </summary>
    [JsonStringEnumMemberName("forward")]
    Forward,
    /// <summary>
    /// Ответ на сообщение
    /// </summary>
    [JsonStringEnumMemberName("reply")]
    Reply
}