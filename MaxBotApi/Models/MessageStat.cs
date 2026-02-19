using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class MessageStat
{
    [JsonPropertyName("views")]
    public long Views { get; set; }

    public override string ToString()
    {
        return string.Format("Views: {0}", Views);
    }
}