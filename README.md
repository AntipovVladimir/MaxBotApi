## .NET Client for Max Bot API

Используется **net10 C#14**, построено для работы через webhook.

Для разработки использовалась следующая документация
https://dev.max.ru/docs-api



Методы:
#### subscriptions
```
SetWebhook(string url, string? secretToken = null, IEnumerable<UpdateType>? updateTypes = null)
DeleteWebhook(string url)
GetWebhookInfo()
GetUpdates(int limit = 100, int timeout = 30, long? marker = null, IEnumerable<UpdateType>? types = null)
```
#### bot
```
GetMe()
```
#### messages
```
GetMessages(long chat_id)
GetMessages(IEnumerable<string> messages_ids)
GetMessages(long chat_id, IEnumerable<string> messages_ids)
SendMessage(long user_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? newMessageLink = null, IEnumberable<AttachmentRequest>? attachments = null)
SendMessageToChat(long chat_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? newMessageLink = null, IEnumberable<AttachmentRequest>? attachments = null)            
EditMessage(string message_id, string? text = null, IEnumberable<AttachmentRequest>? attachments = null, NewMessageLink? link = null,
            bool notify = true, TextFormat text_format = TextFormat.HTML)
DeleteMessage(string messageId)
SendCallbackReact(string callback_id, NewMessageBody? newMessageBody = null, string? notification = null)
GetMessage(string message_id)
GetVideoInfo(string video_token)                        
```
#### chat
```
GetChat(long chat_id)
EditChatInfo(long chat_id, PhotoAttachmentRequestPayload? icon = null, string? title = null, string? pin = null,
            bool? notify = null)
DeleteChat(long chat_id)
SendChatAction(long chat_id, SenderAction action)
GetChatPinnedMessage(long chat_id)
PinMessage(long chat_id, string message_id, bool notify = true)
UnpinMessage(long chat_id)
GetChatMyInfo(long chat_id)
LeaveChat(long chat_id)
GetChatAdmins(long chat_id)
AddChatAdmins(long chat_id, IEnumerable<ChatAdmin> admins)
DeleteChatAdmin(long chat_id, long user_id)
GetChatMembers(long chat_id)
GetChatMembers(long chat_id, long marker, int count = 20)
GetChatMembers(long chat_id, IEnumerable<long> user_ids)
InviteUser(long chat_id, IEnumerable<long> user_ids)
KickUser(long chat_id, long user_id, bool block = false)            
```


#### Пример для minimal api. 

```
using MaxBot;
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
        case MessageEditedUpdate meu:
            await Task.Run(async () => await ProcessMessage(meu.Message, null));
            return;
        case MessageCreatedUpdate mcu:
            await Task.Run(async () => await ProcessMessage(mcu.Message, mcu.UserLocale));
            return;
        case BotRemovedUpdate:
        case BotAddedUpdate:
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
```

#### Данный клиент будет постепенно дорабатываться, документация дополняться. 
#### По вопросам связанным с данным кодом можно писать мне в MAX https://max.ru/join/rGHhNOyyFyG4p2I7IwryhaWPxecPHqykNC0plzA3X2Q
#### либо в телеграм https://t.me/darkagent