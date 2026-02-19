using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SenderAction
{
    [JsonStringEnumMemberName("typing_on")]
    TypingOn,
    [JsonStringEnumMemberName("sending_photo")]
    SendingPhoto,
    [JsonStringEnumMemberName("sending_video")]
    SendingVideo,
    [JsonStringEnumMemberName("sending_audio")]
    SendingAudio,
    [JsonStringEnumMemberName("sending_file")]
    SendingFile,
    [JsonStringEnumMemberName("mark_seen")]
    MarkSeen
}