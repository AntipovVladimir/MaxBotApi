using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetUpdatesRequest : RequestBase<UpdatesResponse>
{
    public GetUpdatesRequest() : base("updates")
    {
        HttpMethod = HttpMethod.Get;
    }

    /// <summary>
    /// Максимальное количество обновлений для получения (1-1000). По умолчанию 100.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Тайм-аут в секундах для долгого опроса
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления
    /// </summary>
    public long? Marker { get; set; }

    /// <summary>Список типов обновлений, которые бот хочет получить (например, message_created, message_callback)</summary>
    [JsonPropertyName("types")]
    public IEnumerable<UpdateType>? Types { get; set; }
}