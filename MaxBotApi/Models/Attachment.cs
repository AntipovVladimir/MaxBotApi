using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models.Payloads;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(PolymorphicJsonConverter<Attachment>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(ImageAttachment), "image")]
[CustomJsonDerivedType(typeof(VideoAttachment), "video")]
[CustomJsonDerivedType(typeof(AudioAttachment), "audio")]
[CustomJsonDerivedType(typeof(FileAttachment), "file")]
[CustomJsonDerivedType(typeof(StickerAttachment), "sticker")]
[CustomJsonDerivedType(typeof(ContactAttachment), "contact")]
[CustomJsonDerivedType(typeof(InlineKeyboardAttachment), "inline_keyboard")]
[CustomJsonDerivedType(typeof(ShareAttachment), "share")]
[CustomJsonDerivedType(typeof(LocationAttachment), "location")]
public abstract class Attachment
{
    /// <summary>
    /// image, video, audio, file, sticker, contact, inline_keyboard, share, location 
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public abstract AttachmentType Type { get; set; }

    public override string ToString()
    {
        return string.Format("Attachment Type: {0}", Type);
    }
}

public class ContactAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Contact;
    [JsonPropertyName("payload")] public required ContactAttachmentPayload Payload { get; set; }
}

public class InlineKeyboardAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.InlineKeyboard;
    [JsonPropertyName("payload")] public required InlineKeyboard Payload { get; set; }
}

public class StickerAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Sticker;
    [JsonPropertyName("payload")] public required StickerAttachmentPayload Payload { get; set; }

    /// <summary>
    /// Ширина стикера
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Высота стикера
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}

public class FileAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.File;
    [JsonPropertyName("payload")] public required FileAttachmentPayload Payload { get; set; }

    /// <summary>
    /// Имя загруженного файла
    /// </summary>
    [JsonPropertyName("filename")]
    public required string Filename { get; set; }

    /// <summary>
    /// Размер файла в байтах
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }
}

public class AudioAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Audio;
    [JsonPropertyName("payload")] public required MediaAttachmentPayload Payload { get; set; }

    /// <summary>
    /// Аудио транскрипция
    /// </summary>
    [JsonPropertyName("transcription")]
    public string? Transcription { get; set; }
}

public class VideoAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Video;
    [JsonPropertyName("payload")] public required MediaAttachmentPayload Payload { get; set; }

    /// <summary>
    /// Миниатюра видео
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public VideoThumbnail? Thumbnail { get; set; }

    /// <summary>
    /// Ширина видео
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }

    /// <summary>
    /// Высота видео
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }

    /// <summary>
    /// Длина видео в секундах
    /// </summary>
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }
}

public class ShareAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Share;
    [JsonPropertyName("payload")] public required ShareAttachmentPayload Payload { get; set; }

    /// <summary>
    /// Заголовок предпросмотра ссылки.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Описание предпросмотра ссылки
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Изображение предпросмотра ссылки
    /// </summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}

public class ImageAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Image;
    [JsonPropertyName("payload")] public required PhotoAttachmentPayload Payload { get; set; }
}

public class LocationAttachment : Attachment
{
    public override AttachmentType Type { get; set; } = AttachmentType.Location;

    /// <summary>
    /// широта
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// долгота
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}