using System.Text;
using MaxBotApi;
using MaxBotApi.Enums;
using MaxBotApi.Models;
using SampleBot;

Settings settings = Settings.Load();

string token = "токен бота";
string serverAddress = "https://адресвашегосервера";
string botPath = "/bot";
string webhookUrl = string.Concat(serverAddress, botPath);
string secret = "проверочныйкод"; // этим кодом будет сопровождаться каждый запрос от платформы
SecretValidationMiddleWare.SetSecretToken(secret, botPath);
var bot = new MaxBotClient(token);
await bot.SetWebhook(webhookUrl, secret);
var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddHttpClient("maxwebhook").RemoveAllLoggers().AddTypedClient(httpClient => bot);

var app = builder.Build();

app.UseMiddleware<SecretValidationMiddleWare>();
app.MapPost(botPath, HandleUpdate);
app.MapPost("/zabbix", ZabbixHandler);
app.Run();
// сюда мы прыгаем когда завершаем работу бота, для корректности убираем за собой привязку вебхука, чтобы платформа не стучалась, пока бот не запущен
await bot.DeleteWebhook(webhookUrl);

async Task HandleUpdate(MaxBotClient _bot, Update update)
{
    switch (update)
    {
        case MessageEditedUpdate meu:
            await Task.Run(async () => await ProcessMessage(meu.Message, null));
            return;
        case MessageCreatedUpdate mcu:
            await Task.Run(async () => await ProcessMessage(mcu.Message, mcu.UserLocale));
            return;
        case BotRemovedUpdate bru:
            await Task.Run(async () => await ProcessBotRemoved(bru));
            break;
        case BotAddedUpdate bau:
            await Task.Run(async () => await ProcessBotAdded(bau));
            break;
        case ChatTitleChangedUpdate:
        case MessageCallbackUpdate:
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

async Task ProcessBotAdded(BotAddedUpdate bau)
{
    long chatId = bau.ChatId;
    if (settings.OwnerId != 0 && bau.User.UserID == settings.OwnerId)
    {
        settings.ZabbixReportChatId = chatId;
        settings.Save();
    }
    else
    {
        await Task.Run(async () => await bot.LeaveChat(chatId));
        System.Diagnostics.Debug.WriteLine("Нас попытались добавить в чат, но добавлял нас не владелец, мы покинули этот чат:");
        System.Diagnostics.Debug.WriteLine(bau.ToString());
    }
}

async Task ProcessBotRemoved(BotRemovedUpdate bru)
{
    if (bru.ChatId == settings.ZabbixReportChatId)
    {
        settings.ZabbixReportChatId = 0;
        settings.Save();
        if (settings.OwnerId != 0)
        {
            if (settings.OwnerId != bru.User.UserID)
                await Task.Run(async () => await bot.SendMessage(settings.OwnerId,
                    string.Format("Покидаем чат {0} по инициативе пользователя {1}, отчеты отключены", bru.ChatId,
                        bru.User.FirstName + " " + bru.User.LastName)));
            else
                await Task.Run(async () =>
                    await bot.SendMessage(settings.OwnerId, string.Format("Покидаем чат {0} по вашей воле, отчеты отключены", bru.ChatId)));
            return;
        }
    }

    System.Diagnostics.Debug.WriteLine("Нас иключили из чата:");
    System.Diagnostics.Debug.WriteLine(bru.ToString());
}


async Task ProcessMessage(Message message, string? userLocale)
{
    if (string.IsNullOrEmpty(message.MessageBody?.Text)) return;
    if (message.Recipient.ChatType == ChatType.Dialog && message.Sender is not null)
    {
        if (message.MessageBody!.Text.StartsWith("/auth"))
        {
            if (settings.OwnerId != 0) return;
            if (message.MessageBody!.Text.Contains("теперь я твой хозяин"))
            {
                settings.OwnerId = message.Sender.UserID;
                settings.Save();
                try
                {
                    await bot.ReplyMessage(message, "да мастер, слушаюсь!");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
    }
    else if (message.Recipient.ChatType == ChatType.Chat)
    {
        // пока игнорируем
    }
}

async Task ZabbixHandler(MaxBotClient _bot, EventUpdate update)
{
    if (settings.ZabbixReportChatId == 0) return;
    StringBuilder sb = new();
    if (update.Subject.StartsWith("Problem"))
        sb.AppendLine("❌ Новая проблема:");
    else if (update.Subject.StartsWith("Resolve"))
        sb.AppendLine("✅ Проблема решена:");
    else
        sb.AppendLine(string.Format("Событие: {0}", update.Subject));
    sb.AppendLine(update.Message);
    await Task.Run(async () =>
        {
            try
            {
                await _bot.SendMessageToChat(settings.ZabbixReportChatId, sb.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    );
}