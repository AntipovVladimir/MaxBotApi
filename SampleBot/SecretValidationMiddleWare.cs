using System.Net;
using System.Text.Json;
using MaxBotApi;

namespace SampleBot;

public class SecretValidationMiddleWare(RequestDelegate next, IMaxBotClient bot, ILogger<SecretValidationMiddleWare> logger)
{
    private const string XSecret = "X-Max-Bot-Api-Secret";
    private static string SecretToken = "";
    private static string BotPath = "/bot";

    /// <summary>
    /// Преднастройка проверочной прослойки
    /// </summary>
    /// <param name="secretToken">секрет заданный для запросов от платформы</param>
    /// <param name="botPath">путь на сервере по которому идут обращению к боту от платформы, по умолчанию "/bot"</param>
    public static void SetSecretToken(string secretToken, string botPath = "/bot")
    {
        SecretToken = secretToken;
        BotPath = botPath;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Мы проверяем secret token только для запросов к обработке bot api, чтобы удостовериться что мы работаем только с платформой, без посторонних
        // по умолчанию смотрим в сторону /bot
        if (context.Request.Path.StartsWithSegments(BotPath) &&
            (!context.Request.Headers.ContainsKey(XSecret) || !context.Request.Headers[XSecret].Equals(SecretToken)))
        {
            logger.LogError("!!!no secret header in request!");
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            var response = new
            {
                Title = "Access Forbidden",
                Status = context.Response.StatusCode
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
            return;
        }

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                Title = "An unexpected error occurred.",
                Status = context.Response.StatusCode,
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}