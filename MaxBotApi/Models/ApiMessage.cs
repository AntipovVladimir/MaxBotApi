using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class ApiMessage
{
    [JsonPropertyName("message")]
    public required Message Message { get; set; }

    public override string ToString()
    {
        return Message.ToString();
    }
}