using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MarkupElementType
{
    [JsonStringEnumMemberName("strong")]
    Strong ,
    [JsonStringEnumMemberName("emphasized")]
    Emphasized,
    [JsonStringEnumMemberName("monospaced")]
    Monospaced,
    [JsonStringEnumMemberName("link")]
    Link,
    [JsonStringEnumMemberName("strikethrough")]
    Strikethrough,
    [JsonStringEnumMemberName("underline")]
    Underline, 
    [JsonStringEnumMemberName("user_mention")]
    UserMention
}
