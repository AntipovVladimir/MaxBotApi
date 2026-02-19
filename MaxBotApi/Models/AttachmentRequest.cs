using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models.Payloads;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(PolymorphicJsonConverter<AttachmentRequest>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(ImageAttachmentRequest), "image")]
[CustomJsonDerivedType(typeof(VideoAttachmentRequest), "video")]
[CustomJsonDerivedType(typeof(AudioAttachmentRequest), "audio")]
[CustomJsonDerivedType(typeof(FileAttachmentRequest), "file")]
[CustomJsonDerivedType(typeof(StickerAttachmentRequest), "sticker")]
[CustomJsonDerivedType(typeof(ContactAttachmentRequest), "contact")]
[CustomJsonDerivedType(typeof(InlineKeyboardAttachmentRequest), "inline_keyboard")]
[CustomJsonDerivedType(typeof(LocationAttachmentRequest), "location")]
[CustomJsonDerivedType(typeof(ShareAttachmentRequest), "share")]

public abstract class AttachmentRequest
{
    /// <summary>
    /// Тип
    /// </summary>
    [JsonPropertyName("type")]
    public abstract AttachmentType Type { get; }
    
}

public class ImageAttachmentRequest : AttachmentRequest
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override AttachmentType Type=>  AttachmentType.Image;
    
    /// <summary>
    /// Запрос на прикрепление изображения (все поля являются взаимоисключающими)
    /// </summary>
    [JsonPropertyName("payload")]
    public required PhotoAttachmentRequestPayload  Payload { get; set; }
    
}

public class VideoAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Video;
    /// <summary>
    /// Это информация, которую вы получите, как только аудио/видео будет загружено
    /// </summary>
    [JsonPropertyName("payload")]
    public required UploadedInfo Payload { get; set; }
} 


public class AudioAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Audio;
    /// <summary>
    /// Это информация, которую вы получите, как только аудио/видео будет загружено
    /// </summary>
    [JsonPropertyName("payload")]
    public required UploadedInfo Payload { get; set; }
} 



public class FileAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.File;
    /// <summary>
    /// Это информация, которую вы получите, как только аудио/видео будет загружено
    /// </summary>
    [JsonPropertyName("payload")]
    public required UploadedInfo Payload { get; set; }
}

public class StickerAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Sticker;
    
    [JsonPropertyName("payload")]
    public required StickerAttachmentRequestPayload Payload { get; set; }
}

public class ContactAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Contact;
    [JsonPropertyName("payload")]
    public required ContactAttachmentRequestPayload Payload { get; set; }
}

public class InlineKeyboardAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.InlineKeyboard;
    [JsonPropertyName("payload")]
    public required InlineKeyboardAttachmentRequestPayload Payload { get; set; }
}

public class LocationAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Location;
    
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


public class ShareAttachmentRequest : AttachmentRequest
{
    public override AttachmentType Type => AttachmentType.Share;
    [JsonPropertyName("payload")]
    public required ShareAttachmentPayload Payload { get; set; }
}
