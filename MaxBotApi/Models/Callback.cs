using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

public class Callback
{
    /// <summary>
    /// Unix-время, когда пользователь нажал кнопку
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime TimeStamp { get; set; }
    
    /// <summary>
    /// Текущий ID клавиатуры
    /// </summary>
    [JsonPropertyName("callback_id")]
    public required string CallbackId { get; set; }
    
    /// <summary>
    /// Токен кнопки
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }
    
    /// <summary>
    /// Пользователь, нажавший на кнопку
    /// </summary>
    [JsonPropertyName("user")]
    public required User User { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Callback Timestamp: ");
        sb.Append(TimeStamp);
        sb.Append(", CallbackId: ");
        sb.Append(CallbackId);
        if (Payload != null)
        {
            sb.Append(", Payload: ");
            sb.Append(Payload);
        }
        sb.Append(", User: ");
        sb.Append(User);
        return sb.ToString();
    }
}