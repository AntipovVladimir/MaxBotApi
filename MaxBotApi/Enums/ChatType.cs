using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatType
{
    [JsonStringEnumMemberName("chat")]
    Chat,
    [JsonStringEnumMemberName("dialog")]
    Dialog,
    [JsonStringEnumMemberName("channel")]
    Channel
}