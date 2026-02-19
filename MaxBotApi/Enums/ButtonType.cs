using System.Text.Json.Serialization;

namespace MaxBotApi.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ButtonType
{
    [JsonStringEnumMemberName("callback")]
    Callback,
    [JsonStringEnumMemberName("link")]
    Link,
    [JsonStringEnumMemberName("request_geo_location")]
    RequestGeoLocation,
    [JsonStringEnumMemberName("request_contact")]
    RequestContact,
    [JsonStringEnumMemberName("open_app")]
    OpenApp,
    [JsonStringEnumMemberName("message")]
    Message,
}