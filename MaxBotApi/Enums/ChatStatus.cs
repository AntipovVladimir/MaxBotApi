using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatStatus
{
    [JsonStringEnumMemberName("active")]
    Active,
    [JsonStringEnumMemberName("removed")]
    Removed,
    [JsonStringEnumMemberName("left")]
    Left,
    [JsonStringEnumMemberName("closed")]
    Closed
}