using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextFormat
{
    [JsonStringEnumMemberName("markdown")]
    Markdown,
    [JsonStringEnumMemberName("html")]
    HTML
}