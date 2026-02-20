using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Requests;

public class EditChatInfoRequest:RequestBase<Chat>
{
    public EditChatInfoRequest(long chat_id) : base(string.Format("chats/{0}", chat_id))
    {
        HttpMethod = HttpMethod.Patch;
    }
    
    /// <summary>
    /// Запрос на прикрепление изображения (все поля являются взаимоисключающими)
    /// </summary>
    [JsonPropertyName("icon")]
    public PhotoAttachmentRequestPayload? Icon { get; set; }
    
    /// <summary>
    /// от 1 до 200 символов
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    /// <summary>
    /// ID сообщения для закрепления в чате. Чтобы удалить закреплённое сообщение, используйте метод unpin
    /// </summary>
    [JsonPropertyName("pin")]
    public string? Pin { get; set; }
    
    /// <summary>
    /// Если true, участники получат системное уведомление об изменении
    /// </summary>
    [JsonPropertyName("notify")]
    public bool? Notify { get; set; }
}