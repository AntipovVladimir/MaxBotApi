## .NET Client for Max Bot API

Используется **net10 C#14**, построено для работы через webhook.

Для разработки использовалась следующая документация
https://dev.max.ru/docs-api

### Доступно в nuget:
https://www.nuget.org/packages/MaxBotApi
```
dotnet add package MaxBotApi
```

## Документация


### Методы:

### subscriptions

#### Подписка на события (регистрация webhook)

```csharp
async Task<ApiRespone> SetWebhook(string url, string? secretToken = null, IEnumerable<UpdateType>? updateTypes = null)
```

Подписывает бота на получение обновлений через WebHook. После вызова этого метода бот будет получать уведомления о новых событиях в чатах на указанный URL. Ваш
сервер должен прослушивать один из следующих портов: 80, 8080, 443, 8443, 16384-32383

+ **url** (string) URL HTTP(S)-эндпойнта вашего бота. Должен начинаться с http(s)://
+ **secret** (string?) от 5 до 256 символов. Cекрет, который должен быть отправлен в заголовке X-Max-Bot-Api-Secret в каждом запросе Webhook. Разрешены только
  символы A-Z, a-z, 0-9, и дефис. Заголовок рекомендован, чтобы запрос поступал из установленного веб-узла.
+ **updateTypes** (IEnumerable<UpdateType>?) - Список типов обновлений, которые ваш бот хочет получать. Если передается null - бот будет получать все возможные
  обновления.
  Возможные типы обновлений на текущий момент:
+
    + MessageCreated,
+
    + MessageCallback,
+
    + MessageEdited,
+
    + MessageRemoved,
+
    + BotAdded,
+
    + BotRemoved,
+
    + DialogMuted,
+
    + DialogUnmuted,
+
    + DialogCleared,
+
    + DialogRemoved,
+
    + UserAdded,
+
    + UserRemoved,
+
    + BotStarted,
+
    + BotStopped,
+
    + ChatTitleChanged

Возвращает **ApiResponse** объект:

```csharp
public class ApiResponse
{
    // true, если запрос был успешным, false в противном случае
    public bool Success { get; set; }

    // Объяснительное сообщение, если результат не был успешным
    public string? Message { get; set; }
}
```

#### Удаление подписки на события

```csharp
async Task<ApiResponse> DeleteWebhook(string url)
```

Отписывает бота от получения обновлений через Webhook. После вызова этого метода бот перестаёт получать уведомления о новых событиях, и становится доступна
доставка уведомлений через API с длительным опросом

+ **url** (string) URL, который нужно удалить из подписок на WebHook.

Возвращает **ApiResponse** объект

#### Запрос действующих подписок на события

```csharp
async Task<Subscriptions> GetWebhookInfo()
```

Если ваш бот получает данные через Webhook, этот метод возвращает список всех подписок. При настройке уведомлений для production-окружения рекомендуем
использовать Webhook.

_Обратите внимание: для отправки вебхуков поддерживается только протокол HTTPS, включая самоподписанные сертификаты. HTTP не поддерживается_

Возвращает **Subscriptions** объект

```csharp
public class Subscriptions 
{
    public Subscription[]? Webhooks { get; set; }
}

public class Subscription
{
    // URL вебхука
    public required string Url { get; set; }

    // время, когда была создана подписка
    public DateTime Time { get; set; }

    // типа обновлений, на которые зарегистрирована подписка
    public UpdateType[]? UpdateTypes { get; set; }    
}
```

#### Запрос обновлений (long-polling)

```csharp
async Task<UpdatesResponse> GetUpdates(int limit = 100, int timeout = 30, long? marker = null, IEnumerable<UpdateType>? types = null)
```

Этот метод можно использовать для получения обновлений при разработке и тестировании, если ваш бот не подписан на Webhook. Для production-окружения рекомендуем
использовать Webhook

Метод использует долгий опрос (long polling). Каждое обновление имеет свой номер последовательности. Свойство marker в ответе указывает на следующее ожидаемое
обновление.

Все предыдущие обновления считаются завершёнными после прохождения параметра marker. Если параметр marker не передан, бот получит все обновления, произошедшие
после последнего подтверждения

+ **limit** (int) Максимальное количество обновлений для получения, по умолчанию 100
+ **timeout** (int) Тайм-аут в секундах для долгого опроса, по умолчанию 30
+ **marker** (long?) Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления
+ **types** (IEnumerable<UpdateType>?) Список типов обновлений, которые ваш бот хочет получать. Если передается null - бот будет получать все возможные
  обновления

Возвращает объект UpdatesResponse

```csharp
public class UpdatesResponse
{
    // Страница обновлений
    public Update[]? Updates { get; set; }
    // Указатель на следующую страницу данных, если есть
    public long? Marker { get; set; }
}
```

Update будет представлен одним из наследуемых классов:

```csharp
MessageCreatedUpdate
MessageCallbackUpdate
MessageEditedUpdate
MessageRemovedUpdate
BotAddedUpdate
BotRemovedUpdate
DialogMutedUpdate
DialogUnmutedUpdate
DialogClearedUpdate
DialogRemovedUpdate
BotStartedUpdate
BotStoppedUpdate
ChatTitleChangedUpdate
```

### bot

#### Получение информации о профиле бота

```csharp
async Task<BotInfo> GetMe()
```

Метод возвращает информацию о боте, который идентифицируется с помощью токена доступа access_token. В ответе приходит объект User с вариантом наследования
BotInfo, который содержит идентификатор бота, его название, никнейм, время последней активности, описание и аватар (при наличии)

### messages

#### Получение сообщений

```csharp
async Task<MessagesResponse> GetMessages(long chat_id)
async Task<MessagesResponse> GetMessages(IEnumerable<string> messages_ids)
```

Метод возвращает информацию о сообщении или массив сообщений из чата. Для выполнения запроса нужно указать один из параметров — chat_id или message_ids:

+ **chat_id** (long) — метод возвращает массив сообщений из указанного чата. Сообщения возвращаются в обратном порядке: последние сообщения будут первыми в
  массиве.
  ID чата, чтобы получить сообщения из определённого чата. Обязательный параметр, если не указан message_ids


+ **message_ids** (IEnumerable<string>) — метод возвращает информацию о запрошенных сообщениях. Можно указать один идентификатор или несколько. Список ID
  сообщений, которые нужно получить (через запятую). Обязательный параметр, если не указан chat_id

Возвращает объект MessagesResponse:

```csharp
public class MessagesResponse
{
    // Массив сообщений
    public required Message[]  Messages { get; set; }
}
```

#### отправка сообщений

```csharp
async Task<ApiMessage> SendMessage(long user_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumberable<AttachmentRequest>? attachments = null)

async Task<ApiMessage> SendMessageToChat(long chat_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumberable<AttachmentRequest>? attachments = null)            
```

используйте SendMessage с указанием user_id, если хотите отправить сообщение пользователю.

используйте SendMessageToChat с указанием chat_id, если хотите отправить сообщение в группу.

+ **user_id** (long) - Если вы хотите отправить сообщение пользователю, укажите его ID
+ **chat_id** (long) - Если сообщение отправляется в чат, укажите его ID
+ **disable_link_preview** (bool) - Если false, сервер не будет генерировать превью для ссылок в тексте сообщения
+ **text** (string?) - Новый текст сообщения, до 4000 символов
+ **attachments** (IEnumerable<AttachmentRequest>?) - Вложения сообщения. Если пусто, все вложения будут удалены
+ **link** (NewMessageLink?) - Ссылка на сообщение (для ответов и репостов)
+ **notify** (bool) - Если false, участники чата не будут уведомлены (по умолчанию true)
+ **text_format** (TextFormat?) - Если установлен, текст сообщения будет форматирован данным способом, возможные варианты **Markdown** или **HTML**

Возвращает объект **ApiMessage**

```csharp
public class ApiMessage
{
    public required Message Message { get; set; }
}
```

#### Редактирование сообщения

```csharp
async Task<ApiResponse> EditMessage(string message_id, string? text = null, IEnumberable<AttachmentRequest>? attachments = null, NewMessageLink? link = null,
            bool notify = true, TextFormat text_format = TextFormat.HTML)
```

Редактирует сообщение в чате. Если поле attachments равно null, вложения текущего сообщения не изменяются. Если в этом поле передан пустой список, все вложения
будут удалены

_С помощью метода можно отредактировать сообщения, которые отправлены менее 24 часов назад_

+ **message_id** (string) - ID редактируемого сообщения
+ **text** (string?) - Новый текст сообщения, до 4000 символов
+ **attachments** (IEnumerable<AttachmentRequest>) - Вложения сообщения. Если пусто, все вложения будут удалены
+ **link** (NewMessageLink?) - Ссылка на сообщение (для ответов и репостов)
+ **notify** (bool) - Если false, участники чата не будут уведомлены (по умолчанию true)
+ **text_format** (TextFormat?) - Если установлен, текст сообщения будет форматирован данным способом, возможные варианты **Markdown** или **HTML**

Возвращает объект **ApiResponse**

#### Удалить сообщение

```csharp
async Task<ApiResponse> DeleteMessage(string messageId)
```

Удаляет сообщение в диалоге или чате, если бот имеет разрешение на удаление сообщений

_С помощью метода можно удалять сообщения, которые отправлены менее 24 часов назад_

+ **message_id** (string) - ID удаляемого сообщения

Возвращает объект **ApiResponse**

#### Ответ на callback

```csharp
async Task<ApiResponse> SendCallbackReact(string callback_id, NewMessageBody? newMessageBody = null, string? notification = null)
```

Этот метод используется для отправки ответа после того, как пользователь нажал на кнопку. Ответом может быть обновленное сообщение и/или одноразовое уведомление
для пользователя

+ **callback_id** (string) - Идентификатор кнопки, по которой пользователь кликнул. Бот получает идентификатор как часть Update с типом **MessageCallbackUpdate
  **.
+ **newMessageBody** (NewMessageBody?) - Заполните это, если хотите изменить текущее сообщение (текст, вложения, кнопки)
+ **notification** (string?) - Заполните это, если хотите просто отправить одноразовое уведомление пользователю (не отображается в десктоп-клиенте, возможно
  временно, но есть в мобильных приложениях)

Возвращает объект **ApiResponse**

#### Получить сообщение

```csharp
async Task<Message> GetMessage(string message_id)
```

Возвращает сообщение по его ID

+ **message_id** (string) - ID сообщения (mid), чтобы получить одно сообщение в чате

Возвращает объект **Message**

```csharp
public class Message
{
    // Пользователь, отправивший сообщение
    public required User Sender { get; set; }
    
    // Получатель сообщения. Может быть пользователем или чатом
    public required Recipient Recipient { get; set; }
    
    /// Время создания сообщения
    public DateTime TimeStamp { get; set; }
    
    // Пересланное или ответное сообщение
    public LinkedMessage? Link { get; set; }
    
    // Содержимое сообщения. Текст + вложения. Может быть null, если сообщение содержит только пересланное сообщение
    public MessageBody? MessageBody { get; set; }
    
    // Статистика сообщения.
    public MessageStat? Stat { get; set; }
    
    // Публичная ссылка на пост в канале. Отсутствует для диалогов и групповых чатов
    public string? Url { get; set; }   
}
```

#### Получить информацию о видео

```csharp
async Task<VideoInfo> GetVideoInfo(string video_token)                        
```

Возвращает подробную информацию о прикреплённом видео. URL-адреса воспроизведения и дополнительные метаданные

+ **video_token** (string) Токен видео-вложения

Возвращает объект **VideoInfo**

```csharp
public class VideoInfo
{
    // Токен видео-вложения
    public required string Token { get; set; }

    // URL-ы для скачивания или воспроизведения видео. Может быть null, если видео недоступно
    public VideoUrls? Urls { get; set; }

    // Миниатюра видео
    public PhotoAttachmentPayload? Thumbnail { get; set; }
    
    // Ширина видео
    public int Width { get; set; }

    // Высота видео
    public int Height { get; set; }

    // Длина видео в секундах
    public int Duration { get; set; }
}
```

### chat

#### Получение списка всех групповых чатов

```csharp
async Task<ChatsResponse> GetChats()
async Task<ChatsResponse> GetChats(int count = 50, long? marker = null)
```

Возвращает список групповых чатов, в которых участвовал бот, информацию о каждом чате и маркер для перехода к следующей странице списка

+ **count** (int) - Количество запрашиваемых чатов, по умолчанию 50
+ **marker** (long?) - Указатель на следующую страницу данных. Для первой страницы передайте null

Возвращает объект **ChatsResponse**

```csharp
public class ChatsResponse
{
    public required IEnumerable<Chat> Chats { get; set; }
}
```

#### Получение информации о групповом чате

```csharp
async Task<Chat> GetChat(long chat_id)
```

Возвращает информацию о групповом чате по его ID

+ **chat_id** (long) - ID запрашиваемого чата

Возвращает объект **Chat**

```csharp
public class Chat
{
    // ID чата
    public long ChatId { get; set; }

    // Тип чата: dialog, chat, channel
    public ChatType Type { get; set; }

    // Статус чата:
    // Active — Бот является активным участником чата.
    // Removed — Бот был удалён из чата.
    // Left — Бот покинул чат.
    // Closed — Чат был закрыт.
    public ChatStatus Status { get; set; }
    
    // Отображаемое название чата. Может быть null для диалогов
    public string? Title { get; set; }
    
    // Время последнего события в чате
    public DateTime LastEventTime { get; set; }
    
    // Количество участников чата. Для диалогов всегда 2
    public int ParticipantsCount { get; set; }
    
    // ID владельца чата
    public long? OwnerId { get; set; }
    
    // Доступен ли чат публично (для диалогов всегда false)
    public bool IsPublic { get; set; }
    
    // Ссылка на чат
    public string? Link { get; set; }
    
    // Описание чата
    public string? Description { get; set; }

    // Данные о пользователе в диалоге (только для чатов типа "dialog")
    public UserWithPhoto? DialogWithUser { get; set; }
    
    // ID сообщения, содержащего кнопку, через которую был инициирован чат
    public string? ChatMessageId { get; set; }
    
    // Закреплённое сообщение в чате (возвращается только при запросе конкретного чата)
    public Message? PinnedMessage { get; set; }
}
```

#### Изменение информации о групповом чате

```csharp
async Task<Chat> EditChatInfo(long chat_id, PhotoAttachmentRequestPayload? icon = null, string? title = null, string? pin = null,
            bool? notify = null)
```

Позволяет редактировать информацию о групповом чате, включая название, иконку и закреплённое сообщение

+ **chat_id** (long) - ID редактируемого чата
+ **icon** (PhotoAttachmentRequestPayload?) - Запрос на прикрепление изображения (все поля являются взаимоисключающими)
+ **title** (string?) - от 1 до 200 символов
+ **pin** (string?) - ID сообщения для закрепления в чате. Чтобы удалить закреплённое сообщение, используйте метод **UnpinMessage**
+ **notify** (bool?) - Если true, участники получат системное уведомление об изменении

Возвращает объект **Chat**

#### Удаление группового чата

```csharp
async Task<ApiResponse>DeleteChat(long chat_id)
```

Удаляет групповой чат для всех участников

+ **chat_id** (long) - ID удаляемого чата

Возвращает объект **ApiResponse**

#### Отправка действия бота в групповой чат

```csharp
async Task<ApiResponse> SendChatAction(long chat_id, SenderAction action)
```

Позволяет отправлять в групповой чат такие действия бота, как например: «набор текста» или «отправка фото»

+ **chat_id** (long) - ID группового чата
+ **action** (SenderAction) - Действие, отправляемое участникам чата. Возможные значения:
+
    + TypingOn, — Бот набирает сообщение.
+
    + SendingPhoto, — Бот отправляет фото.
+
    + SendingVideo, — Бот отправляет видео.
+
    + SendingAudio, — Бот отправляет аудиофайл.
+
    + SendingFile, — Бот отправляет файл.
+
    + MarkSeen — Бот помечает сообщения как прочитанные.

Возвращает объект **ApiResponse**

#### Получение закреплённого сообщения в групповом чате

```csharp
async Task<ApiMessage> GetChatPinnedMessage(long chat_id)
```

Возвращает закреплённое сообщение в групповом чате

+ **chat_id** (long) - ID группового чата

Возвращает объект **ApiMessage**

#### Закрепление сообщения в групповом чате

```csharp
async Task<ApiResponse> PinMessage(long chat_id, string message_id, bool notify = true)
```

+ **chat_id** (long) - ID группового чата
+ **message_id** (string) - ID сообщения, которое нужно закрепить. Соответствует полю Message.MessageBody.MessageId
+ **notify** (bool) - Если true, участники получат уведомление с системным сообщением о закреплении

Возвращает объект **ApiResponse**

#### Удаление закреплённого сообщения в групповом чате

```csharp
async Task<ApiResponse> UnpinMessage(long chat_id)
```

Удаляет закреплённое сообщение в групповом чате

+ **chat_id** (long) - ID группового чата

Возвращает объект **ApiResponse**

#### Получение информации о членстве бота в групповом чате

```csharp
async Task<ChatMember> GetChatMyInfo(long chat_id)
```

Возвращает информацию о членстве текущего бота в групповом чате. Бот идентифицируется с помощью токена доступа

+ **chat_id** (long) - ID группового чата

Возвращает наследованный от **User** объект **ChatMember**

#### Удаление бота из группового чата

```csharp
async Task<ApiResponse> LeaveChat(long chat_id)
```

Удаляет бота из участников группового чата

+ **chat_id** (long) - ID группового чата

Возвращает объект **ApiResponse**

#### Получение списка администраторов группового чата

```csharp
async TaskChatMembersResponse> GetChatAdmins(long chat_id, long? marker = null)
```

Возвращает список всех администраторов группового чата. Бот должен быть администратором в запрашиваемом чате

+ **chat_id** (long) - ID группового чата
+ **marker** (long?) - Указатель на следующую страницу данных

Возвращает объект **ChatMembersResponse**

```csharp
public class ChatMembersResponse
{
    // Список участников чата с информацией о времени последней активности
    public required ChatMember[] ChatMembers { get; set; }
    
    // Указатель на следующую страницу данных
    public long? Marker  { get; set; }    
}
```

#### Назначить администратора группового чата

```csharp
async Task<ApiResponse> AddChatAdmins(long chat_id, IEnumerable<ChatAdmin> admins)
```

Возвращает значение true, если в групповой чат добавлены все администраторы.

_В группе может быть не более 50 администраторов_

+ **chat_id** (long) - ID группового чата
+ **admins** (IEnumerable<ChatAdmin)) - Список пользователей, которые получат права администратора чата

Возвращает объект **ApiResponse**

#### Отменить права администратора в групповом чате

```csharp
async Task<ApiResponse> DeleteChatAdmin(long chat_id, long user_id)
```

Отменяет права администратора у пользователя в групповом чате, лишая его административных привилегий

+ **chat_id** (long) - ID группового чата
+ **user_id** (long) - ID пользователя

Возвращает объект **ApiResponse**

#### Получение участников группового чата

```csharp
async Task<ChatMembersResponse> GetChatMembers(long chat_id)
async Task<ChatMembersResponse> GetChatMembers(long chat_id, long marker, int count = 20)
async Task<ChatMembersResponse> GetChatMembers(long chat_id, IEnumerable<long> user_ids)
```

Возвращает список участников группового чата

+ **chat_id** (long) - ID группового чата
+ **user_ids** (IEnumerable<long>) - Список ID пользователей, чье членство нужно получить. Когда этот параметр передан, параметры count и marker игнорируются
+ **marker** (long) - Указатель на следующую страницу данных
+ **count** (int) - Количество участников, которых нужно вернуть, по умолчанию 20

Возвращает объект **ChatMembersReponse**

#### Добавление участников в групповой чат

```csharp
async Task<ApiResponse> InviteUser(long chat_id, IEnumerable<long> user_ids)
```

Добавляет участников в групповой чат. Для этого могут потребоваться дополнительные права

+ **chat_id** (long) - ID группового чата
+ **user_ids** (IEnumerable<long>) - Список ID пользователей для добавления в чат

Возвращает объект **ApiResponse**

#### Удаление участника из группового чата

```csharp
KickUser(long chat_id, long user_id, bool block = false)            
```

Удаляет участника из группового чата. Для этого могут потребоваться дополнительные права

+ **chat_id** (long) - ID группового чата
+ **user_id** (long) - ID пользователя, которого нужно удалить из чата
+ **block** (bool) - Если установлено в true, пользователь будет заблокирован в чате. Применяется только для чатов с публичной или приватной ссылкой.
  Игнорируется в остальных случаях

### Upload
####
```csharp
async Task<UploadDataResponse> UploadFile(UploadType type, string filename)       
```
Делает полную операцию по загрузке файла: получет ссылку на загрузку и по ней загружает файл
+ **type** (UploadType) - тип загружаемого файла, может быть Image, Video, Audio, File
+ **filename** (string) - путь до загружаемого файла

#### Типы файлов:
+ + image: JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
+ + video: MP4, MOV, MKV, WEBM, MATROSKA
+ + audio: MP3, WAV, M4A и другие
+ + file: любые типы файлов


Может вызывать исключение типа **FileNotFoundException**.
Возвращает объект **UploadDataResponse**
```csharp
public class UploadDataResponse
{
    // Токен, используемый для прикрепления в AttachmentRequest, не используется при UploadType=Image
    public string? Token { get; set; }

    // Токены, полученные после загрузки изображений, только для UploadType=Image
    public Dictionary<string, UploadedInfo>? Photos { get; set; }
}

public class UploadedInfo
{
    // Токен — уникальный ID загруженного медиафайла
    public required string Token { get; set; }
}
```

#### Внимание!!! На текущий момент в API нет метода для разблокировки заблокированных пользователей, пока это могут делать только администраторы вручную (Ticket: 4009805)

### Пример для minimal api.

```csharp
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
        // пример отправки ботом картинки test.png в ответ на слово test
        if (message.MessageBody.Text is "test")
        {
            try
            {
                var data = await bot.UploadFile(UploadType.Image, "test.png");
                if (data.Photos is not null)
                {
                    List<AttachmentRequest> attachments = [];
                    foreach (var photo in data.Photos)
                    {
                        attachments.Add(new ImageAttachmentRequest()
                        {
                            Payload = new PhotoAttachmentRequestPayload() { Token = photo.Value.Token }
                        });
                    }

                    await bot.SendMessageToChat(message.Recipient.ChatId, "reply", false, false, TextFormat.HTML,
                        new NewMessageLink() { MessageId = message.MessageBody.MessageId, Type = MessageLinkType.Reply },
                        attachments);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message)
            }
        } else {
            await maxbot.SendMessageToChat(message.Recipient.ChatId, message.MessageBody?.Text);
        }        
    }    
}
```

### Данный клиент будет постепенно дорабатываться, документация дополняться.

###### По вопросам связанным с данным кодом можно писать мне в MAX https://max.ru/join/rGHhNOyyFyG4p2I7IwryhaWPxecPHqykNC0plzA3X2Q

###### либо в телеграм https://t.me/darkagent