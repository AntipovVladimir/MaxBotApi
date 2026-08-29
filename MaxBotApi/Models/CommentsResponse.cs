using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class CommentsResponse
{
    /// <summary>
    /// Массив комментариев
    /// </summary>
    [JsonPropertyName("messages")]
    public required CommentMessage[] Messages { get; set; }

    public override string ToString() => this.SerializeToString();
}