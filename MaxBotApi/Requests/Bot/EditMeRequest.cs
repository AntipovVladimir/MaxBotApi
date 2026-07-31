using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Models.Payloads;

namespace MaxBotApi.Requests;

public class EditMeRequest : RequestBase<CommandsResponse>
{
    /// <summary>
    /// Отображаемое имя бота
    /// </summary>
    [Obsolete("Согласно документации, изменить можно только BotCommands: https://dev.max.ru/docs-api/methods/PATCH/me/commands")]
    [JsonIgnore]
    public string? Name { get; set; }

    /// <summary>
    /// Описание бота
    /// </summary>
    [Obsolete("Согласно документации, изменить можно только BotCommands: https://dev.max.ru/docs-api/methods/PATCH/me/commands")]
    [JsonIgnore]
    public string? Description { get; set; }

    /// <summary>
    /// Доступные команды бота
    /// </summary>
    [JsonPropertyName("commands")]
    public IEnumerable<BotCommand>? Commands { get; set; }

    /// <summary>
    /// Картинка для профиля бота
    /// </summary>
    [Obsolete("Согласно документации, изменить можно только BotCommands: https://dev.max.ru/docs-api/methods/PATCH/me/commands")]
    [JsonPropertyName("photo")]
    public PhotoAttachmentRequestPayload? Photo { get; set; }

    public EditMeRequest() : base("me/commands")
    {
        HttpMethod = HttpMethod.Patch;
    }
}