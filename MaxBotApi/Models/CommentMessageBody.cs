using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class CommentMessageBody
{
    /// <summary>
    /// Уникальный ID комментария
    /// </summary>
    [JsonPropertyName("mid")]
    public required string MessageId { get; set; }
    
    /// <summary>
    /// Порядковый номер расположения комментария в посте
    /// </summary>
    [JsonPropertyName("seq")]
    public long SequenceId { get; set; }

    /// <summary>
    /// Текст комментария
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    
    /// <summary>
    /// Разметка текста комментария. Обратите внимание: в тексте комментариев не поддерживаются гиперссылки и упоминание пользователя
    /// </summary>
    [JsonPropertyName("markup")]
    public MarkupElement[]? Markup { get; set; }

}