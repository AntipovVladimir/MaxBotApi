using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(PolymorphicJsonConverter<Button>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(MessageButton), "message")]
[CustomJsonDerivedType(typeof(LinkButton), "link")]
[CustomJsonDerivedType(typeof(CallbackButton), "callback")]
[CustomJsonDerivedType(typeof(RequestGeoLocationButton), "request_geo_location")]
[CustomJsonDerivedType(typeof(RequestContactButton), "request_contact")]
[CustomJsonDerivedType(typeof(OpenAppButton), "open_app")]
public abstract class Button
{
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ButtonType Type { get; }

    
    public static Button WithCallbackData(string text, string callbackData) =>
        new CallbackButton(){ Text= text, Payload = callbackData };
    
}

public class MessageButton : Button
{
    public override ButtonType Type => ButtonType.Message;
    
    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class OpenAppButton : Button
{
    public override ButtonType Type => ButtonType.OpenApp;
    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// Публичное имя (username) бота или ссылка на него, чьё мини-приложение надо запустить
    /// </summary>
    [JsonPropertyName("web_app")]
    public string WebApp { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор бота, чьё мини-приложение надо запустить
    /// </summary>
    [JsonPropertyName("contact_id")]
    public long ContactId { get; set; }

    /// <summary>
    /// Параметр запуска, который будет передан в initData мини-приложения
    /// </summary>
    [JsonPropertyName("payload")]
    public string Payload { get; set; } = string.Empty;
}

public class RequestContactButton : Button
{
    public override ButtonType Type => ButtonType.RequestContact;
    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class RequestGeoLocationButton : Button
{
    public override ButtonType Type => ButtonType.RequestGeoLocation;

    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// Если true, отправляет местоположение без запроса подтверждения пользователя
    /// </summary>
    [JsonPropertyName("quick")]
    public bool Quick { get; set; }
}

public class LinkButton : Button
{
    public override ButtonType Type => ButtonType.Link;

    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// до 2048 символов
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

public class CallbackButton : Button
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ButtonType Type => ButtonType.Callback;

    /// <summary>
    /// от 1 до 128 символов
    /// Видимый текст кнопки
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// до 1024 символов
    /// Токен кнопки
    /// </summary>
    [JsonPropertyName("payload")]
    public required string Payload { get; set; }
}