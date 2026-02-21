using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ButtonType
{
    /// <summary>
    /// Кнопка обработки события
    /// </summary>
    [JsonStringEnumMemberName("callback")]
    Callback,
    /// <summary>
    /// Кнопка-ссылка
    /// </summary>
    [JsonStringEnumMemberName("link")]
    Link,
    /// <summary>
    /// Кнопка запроса геолокации
    /// </summary>
    [JsonStringEnumMemberName("request_geo_location")]
    RequestGeoLocation,
    /// <summary>
    /// Кнопка запроса контакта
    /// </summary>
    [JsonStringEnumMemberName("request_contact")]
    RequestContact,
    /// <summary>
    /// Кнопка открытия приложения
    /// </summary>
    [JsonStringEnumMemberName("open_app")]
    OpenApp,
    /// <summary>
    /// Кнопка сообщения (текст кнопки будет использован как выбор ответа в сообщении при нажатии)
    /// </summary>
    [JsonStringEnumMemberName("message")]
    Message,
}