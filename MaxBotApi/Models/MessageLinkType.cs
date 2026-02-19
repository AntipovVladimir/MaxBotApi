using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageLinkType
{
    [JsonStringEnumMemberName("forward")]
    Forward,
    [JsonStringEnumMemberName("reply")]
    Reply
}