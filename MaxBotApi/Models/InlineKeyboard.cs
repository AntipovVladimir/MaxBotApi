using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class InlineKeyboard
{
    /// <summary>
    /// двумерный массив кнопок ([строка][столбец])
    /// </summary>
    [JsonPropertyName("buttons")]
    public Button[][] Buttons { get; set; } = [];
}