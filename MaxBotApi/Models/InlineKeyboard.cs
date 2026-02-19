using System.Text.Json.Serialization;

namespace MaxBotApi.Models;

public class InlineKeyboard
{
    [JsonPropertyName("buttons")] public Button[][] Buttons { get; set; } = [];
}