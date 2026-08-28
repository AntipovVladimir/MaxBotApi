using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class ApiCommentMessage
{
    [JsonPropertyName("message")] 
    public required CommentMessage Message { get; set; }

    public override string ToString() => this.SerializeToString();
}