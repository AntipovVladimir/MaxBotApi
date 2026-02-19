using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class SetWebhookRequest : RequestBase<ApiResponse>
{
    public SetWebhookRequest() : base("subscriptions")
    {
        HttpMethod = HttpMethod.Post;
    }

    /// <summary>
    /// URL HTTP(S)-эндпойнта вашего бота. Должен начинаться с http(s)://
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <summary>
    /// Список типов обновлений, которые ваш бот хочет получать. Для полного списка типов см. объект Update
    /// </summary>
    [JsonPropertyName("update_types")]
    public required UpdateType[] UpdateTypes { get; set; }

    /// <summary>
    /// Cекрет, который должен быть отправлен в заголовке X-Max-Bot-Api-Secret в каждом запросе Webhook. Разрешены только символы A-Z, a-z, 0-9, и дефис. Заголовок рекомендован, чтобы запрос поступал из установленного веб-узла
    /// </summary>
    [JsonPropertyName("secret")]
    public string? Secret { get; set; }
}