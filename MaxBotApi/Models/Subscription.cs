using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class Subscriptions
{
    [JsonPropertyName("subscriptions")] public Subscription[]? Webhooks { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine("Webhooks registered:");
        if (Webhooks is not null)
        {
            foreach (Subscription hook in Webhooks)
                sb.AppendLine(hook.ToString());
        }

        return sb.ToString();
    }
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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Url: ");
        sb.Append(Url);
        sb.Append(", Time: ");
        sb.Append(Time);
        if (UpdateTypes is not null)
        {
            sb.AppendLine(", UpdateTypes: ");
            foreach (UpdateType updateType in UpdateTypes)
                sb.Append(updateType.ToString());

            sb.AppendLine();
        }

        return sb.ToString();
    }
}