using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetUpdatesRequest : RequestBase<UpdatesResponse>
{
    public GetUpdatesRequest() : base("updates")
    {
        HttpMethod =  HttpMethod.Get;
    }

    /// <summary>Limits the number of updates to be retrieved. Values between 1-100 are accepted. Defaults to 100.</summary>
    public int? Limit { get; set; }

    /// <summary>Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling should be used for testing purposes only.</summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления
    /// </summary>
    public long? Marker { get; set; }
    
    /// <summary>Список типов обновлений, которые бот хочет получить (например, message_created, message_callback)</summary>
    [JsonPropertyName("types")]
    public IEnumerable<UpdateType>? Types { get; set; }
} 