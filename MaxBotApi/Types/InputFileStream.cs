using System.Text.Json.Serialization;

namespace MaxBotApi.Types;

public class InputFileStream
{
    public required Stream Content { get; set; }

    [JsonPropertyName("data")]
    public required string FileName { get; set; }
}