using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AttachmentType
{
    [JsonStringEnumMemberName("image")] Image,
    [JsonStringEnumMemberName("video")] Video,
    [JsonStringEnumMemberName("audio")] Audio,
    [JsonStringEnumMemberName("file")] File,
    [JsonStringEnumMemberName("sticker")] Sticker,
    [JsonStringEnumMemberName("contact")] Contact,
    [JsonStringEnumMemberName("inline_keyboard")]
    InlineKeyboard,
    [JsonStringEnumMemberName("share")] Share,
    [JsonStringEnumMemberName("location")] Location,
}