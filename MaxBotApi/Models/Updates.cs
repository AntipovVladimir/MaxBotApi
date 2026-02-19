using System.Text.Json.Serialization;

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
}