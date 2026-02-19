using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;
using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class EditMessageRequest : RequestBase<ApiResponse>
{
    public EditMessageRequest(string messageId) : base(string.Format("messages?message_id={0}", messageId))
    {
        HttpMethod = HttpMethod.Put;
    }

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
    public AttachmentRequest[]? Attachments { get; set; }

    /// <summary>
    /// Ссылка на сообщение
    /// </summary>
    [JsonPropertyName("link")]
    public NewMessageLink? Link { get; set; }

    /// <summary>
    /// Если false, участники чата не будут уведомлены (по умолчанию true)
    /// </summary>
    [JsonPropertyName("notify")]
    public bool Notify { get; set; }

    /// <summary>
    /// Enum: "markdown" "html"
    /// Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел
    /// </summary>
    [JsonPropertyName("format")]
    public TextFormat? TextFormat { get; set; }
}