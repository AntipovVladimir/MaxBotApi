using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class ApiMessage
{
    [JsonPropertyName("message")]
    public required Message Message { get; set; }

    public override string ToString() => this.SerializeToString();
}