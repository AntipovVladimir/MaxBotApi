using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Models.Payloads;
using MaxBotApi.Types;

namespace MaxBotApi.Requests;

public class EditMeRequest : RequestBase<ApiResponse>
{
    /// <summary>
    /// Отображаемое имя бота
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Описание бота
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Доступные команды бота
    /// </summary>
    [JsonPropertyName("commands")]
    public IEnumerable<BotCommand>? Commands { get; set; }

    /// <summary>
    /// Картинка для профиля бота
    /// </summary>
    [JsonPropertyName("photo")]
    public PhotoAttachmentRequestPayload? Photo { get; set; }

    public EditMeRequest() : base("me")
    {
        HttpMethod = HttpMethod.Patch;
    }
}