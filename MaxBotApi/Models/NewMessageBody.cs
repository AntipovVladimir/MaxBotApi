using System.Text.Json.Serialization;
using MaxBotApi.Enums;

namespace MaxBotApi.Models;

public class NewMessageBody
{
    /// <summary>
    /// до 4000 символов
    /// Новый текст сообщения
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Вложения сообщения. Если пусто, все вложения будут удалены
    /// </summary>
    [JsonPropertyName("attachments")]
    public IEnumerable<AttachmentRequest>? Attachments { get; set; }

    /// <summary>
    /// Ссылка на сообщение
    /// </summary>
    [JsonPropertyName("link")]
    public NewMessageLink? Link { get; set; }

    /// <summary>
    /// По умолчанию: true
    /// Если false, участники чата не будут уведомлены (по умолчанию true)
    /// </summary>
    [JsonPropertyName("notify")]
    public bool Notify { get; set; }
    
    [JsonPropertyName("format")]
    public TextFormat? TextFormat { get; set; }
}