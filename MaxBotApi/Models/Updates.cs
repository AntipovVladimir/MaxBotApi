using System.Text.Json.Serialization;
using MaxBotApi.Extensions;

namespace MaxBotApi.Models;

public class UpdatesResponse
{
    /// <summary>
    /// Страница обновлений
    /// </summary>
    [JsonPropertyName("updates")]
    public Update[]? Updates { get; set; }
    
    /// <summary>
    /// Указатель на следующую страницу данных
    /// </summary>
    [JsonPropertyName("marker")]
    public long? Marker { get; set; }
    public override string ToString() => this.SerializeToString();
}