using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class MessageStat
{
    [JsonPropertyName("views")]
    public long Views { get; set; }

    public override string ToString() => this.SerializeToString();
}