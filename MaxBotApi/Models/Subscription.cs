using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Extensions;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class Subscriptions
{
    [JsonPropertyName("subscriptions")] public Subscription[]? Webhooks { get; set; }

    public override string ToString() => this.SerializeToString();
}

public class Subscription
{
    /// <summary>
    /// URL вебхука
    /// </summary>
    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; set; }

    /// <summary>
    /// Unix-время, когда была создана подписка
    /// </summary>
    [JsonPropertyName("time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonPropertyName("update_types")] public UpdateType[]? UpdateTypes { get; set; }

    public override string ToString() => this.SerializeToString();
}