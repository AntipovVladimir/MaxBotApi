using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class SendMessageToUserRequest : SendMessageRequest
{
    private const string base1 = "messages?user_id={0}";
    private const string base2 = "messages?user_id={0}&disable_link_preview=true";

    public SendMessageToUserRequest(long userId, bool disable_link_preview) : base(string.Format(disable_link_preview ? base2 : base1, userId))
    {
        HttpMethod = HttpMethod.Post;
    }
}

public class SendMessageToChatRequest : SendMessageRequest
{
    private const string base1 = "messages?chat_id={0}";
    private const string base2 = "messages?chat_id={0}&disable_link_preview=true";

    public SendMessageToChatRequest(long chatId, bool disable_link_preview) : base(string.Format(disable_link_preview ? base2 : base1, chatId))
    {
        HttpMethod = HttpMethod.Post;
    }
}

public class SendMessageRequest(string urloptions) : RequestBase<ApiMessage>(urloptions)
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
    public bool Notify { get; set; } = true;

    /// <summary>
    /// Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0
    /// </summary>
    [JsonPropertyName("format")]
    public TextFormat? Format { get; set; }
}