**.NET Client for Max Bot API**

Используется **net10 C#14**, построено для работы через webhook.

Для разработки использовалась следующая документация
https://dev.max.ru/docs-api


Пример для minimal api. 

```using MaxBot;
using MaxBotApi;
using MaxBotApi.Enums;
using MaxBotApi.Models;
string token = "токен бота";
string webhookurl = "https://адресвашегобота/bot";
string secret = "проверочныйкод"; // этим кодом будет сопровождаться каждый запрос от платформы
var bot = new MaxBotClient(token);
await bot.SetWebhook(webhookurl, secret);

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddHttpClient("maxwebhook").RemoveAllLoggers().AddTypedClient(httpClient => bot);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/bot", HandleUpdate);
app.Run();
await bot.DeleteWebhook(webhookUrl);

async Task HandleUpdate(MaxBotClient _bot, Update update)
{
    Log.Info(update.ToString());
    switch (update)
    {
        case BotRemovedUpdate bru:
            await m_task_factory.StartNew(async () => await ProcessBotRemoved(bru));
            return;
        case BotAddedUpdate bau:
            await m_task_factory.StartNew(async () => await ProcessBotAdded(bau));
            return;
        case ChatTitleChangedUpdate ctcu:
            await m_task_factory.StartNew(async () => await ProcessChatTitleChangeUpdate(ctcu));
            return;
        case MessageCallbackUpdate mcbu:
            await m_task_factory.StartNew(async () => await ProcessCallback(mcbu));
            return;
        case MessageEditedUpdate meu:
            await m_task_factory.StartNew(async () => await ProcessMessage(meu.Message, null));
            return;
        case MessageCreatedUpdate mcu:
            await m_task_factory.StartNew(async () => await ProcessMessage(mcu.Message, mcu.UserLocale));
            return;
        case UserAddedUpdate:
        case UserRemovedUpdate:
        case MessageRemovedUpdate:
        case DialogMutedUpdate:
        case DialogUnmutedUpdate:
        case DialogClearedUpdate:
        case DialogRemovedUpdate:
        case BotStartedUpdate:
        case BotStoppedUpdate:
            break;
    }
}


async Task ProcessMessage(Message message, string? userLocale)
{
    if (string.IsNullOrEmpty(message.MessageBody?.Text)) return;
    if (message.Recipient.ChatType == ChatType.Dialog)
    {
        await bot.SendMessage(message.Sender.UserID, message.MessageBody?.Text);
    } else if (message.Recipient.ChatType == ChatType.Chat)
    {
        await maxbot.SendMessageToChat(message.Recipient.ChatId, message.MessageBody?.Text);
    }    
}