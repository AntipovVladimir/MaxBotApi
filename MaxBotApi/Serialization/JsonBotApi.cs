using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxBotApi.Serialization;

/// <summary>Static class offering JsonSerializerOptions configured for Bot API serialization</summary>
public static class JsonBotAPI
{
    /// <summary>JsonSerializerOptions configured for Bot API serialization</summary>
    public static JsonSerializerOptions Options { get; }

    static JsonBotAPI() => Configure(Options = new());

    /// <summary>Configure JsonSerializerOptions for Bot API serialization</summary>
    /// <param name="options">JsonSerializerOptions to configure</param>
    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.AllowOutOfOrderMetadataProperties = true;
        options.IncludeFields = true;
    }
}