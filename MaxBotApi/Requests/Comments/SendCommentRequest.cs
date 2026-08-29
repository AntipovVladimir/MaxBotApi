using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class SendCommentRequest : RequestBase<ApiCommentMessage>
{
    public SendCommentRequest(string messageId, bool? disable_link_preview) : base(string.Empty)
    {
        MethodName = disable_link_preview.HasValue && disable_link_preview.Value
            ? string.Format("messages/{0}/comments?disable_link_preview=true", messageId)
            : string.Format("messages/{0}/comments", messageId);
        HttpMethod = HttpMethod.Post;
    }

    /// <summary>
    /// до 4000 символов
    /// Новый текст сообщения
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Ссылка на комментарий?
    /// </summary>
    [JsonPropertyName("link")]
    public NewMessageLink? Link { get; set; }

    /// <summary>
    /// Возможные значения в enum: "markdown" "html". Разметка текста комментария. Для комментариев не поддерживается упоминание других пользователей и гиперссылки.
    /// </summary>
    [JsonPropertyName("format")]
    public TextFormat? Format { get; set; }
}