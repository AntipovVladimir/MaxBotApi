using System.Text.Json.Serialization;
using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class PinMessageRequest : RequestBase<ApiResponse>
{
    public PinMessageRequest(long chatId) : base(string.Format("chats/{0}/pin", chatId))
    {
        HttpMethod = HttpMethod.Put;
    }

    /// <summary>
    /// ID сообщения, которое нужно закрепить. Соответствует полю Message.body.mid
    /// </summary>
    [JsonPropertyName("message_id")]
    public required string MessageId { get; set; }
    
    /// <summary>
    /// Если true, участники получат уведомление с системным сообщением о закреплении
    /// </summary>
    [JsonPropertyName("notify")]
    public bool? Notify { get; set; }
}