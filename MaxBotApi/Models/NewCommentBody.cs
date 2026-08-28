using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class NewCommentBody
{
    /// <summary>
    /// до 40000 символов. текст комментария
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// ссылка на комментарий
    /// </summary>
    [JsonPropertyName("link")]
    public NewMessageLink? Link { get; set; }

    /// <summary>
    /// Возможные значения в enum: "markdown" "html". Разметка текста комментария. Для комментариев не поддерживается упоминание других пользователей и гиперссылки.
    /// </summary>
    [JsonPropertyName("format")]
    public TextFormat? Format { get; set; }
    
    public override string ToString() => this.SerializeToString();
}