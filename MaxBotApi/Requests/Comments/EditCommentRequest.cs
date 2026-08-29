using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class EditCommentRequest : RequestBase<ApiResponse>
{
    public EditCommentRequest(string messageId, string commentId) : base(string.Format("messages/{0}/comments?comment_id={1}", messageId, commentId))
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