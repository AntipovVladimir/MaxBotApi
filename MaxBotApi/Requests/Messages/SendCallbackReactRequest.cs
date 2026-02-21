using System.Text.Json.Serialization;
using MaxBotApi.Exceptions;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class SendCallbackReactRequest : RequestBase<ApiResponse>
{
    public SendCallbackReactRequest(string callbackId) : base(string.Format("answers?callback_id={0}", callbackId))
    {
        HttpMethod = HttpMethod.Post;
    }

    /// <summary>
    /// до 4000 символов
    /// Новый текст сообщения
    /// </summary>
    [JsonPropertyName("message")]
    public NewMessageBody? Message { get; set; }


    /// <summary>
    /// Заполните это, если хотите просто отправить одноразовое уведомление пользователю
    /// </summary>
    [JsonPropertyName("notification")]
    public string? Notification { get; set; }
}