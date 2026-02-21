using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SenderAction
{
    /// <summary>
    /// печатает...
    /// </summary>
    [JsonStringEnumMemberName("typing_on")]
    TypingOn,
    /// <summary>
    /// отправляет фото..
    /// </summary>
    [JsonStringEnumMemberName("sending_photo")]
    SendingPhoto,
    /// <summary>
    /// отправляет видео...
    /// </summary>
    [JsonStringEnumMemberName("sending_video")]
    SendingVideo,
    /// <summary>
    /// отправляет аудио/голосовое
    /// </summary>
    [JsonStringEnumMemberName("sending_audio")]
    SendingAudio,
    /// <summary>
    /// отправляет файл
    /// </summary>
    [JsonStringEnumMemberName("sending_file")]
    SendingFile,
    /// <summary>
    /// отмечает сообщения прочитанными
    /// </summary>
    [JsonStringEnumMemberName("mark_seen")]
    MarkSeen
}