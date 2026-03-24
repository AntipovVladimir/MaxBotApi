## .NET Client for Max Bot API

Используется **net10 C#14**, построено для работы через webhook.

Для разработки использовалась следующая документация
https://dev.max.ru/docs-api



---

### Доступно в nuget:
https://www.nuget.org/packages/MaxBotApi

<details>
<summary>Установка</summary>

#### установка через .NET CLi
```
dotnet add package MaxBotApi
```
#### установка через NuGet package manager
```
Install-Package MaxBotApi
```

</details>

---

### Реализованы методы API
### bots
|          | метод         | описание                                      |
|----------|---------------|-----------------------------------------------|
| &#10004; | **GET** /me   | [Получение информации о боте](#method-getme)  |
| &#10004; | **PATCH** /me | [Изменение информации о боте](#method-editme) |

### messages
|          | метод                           | описание                                            |
|----------|---------------------------------|-----------------------------------------------------|
| &#10004; | **GET** /messages               | [Получение сообщений](#method-getmessages)          |
| &#10004; | **POST** /messages              | [Отправить сообщение](#method-sendmessage)          |
| &#10004; | **PUT** /messages               | [Редактирование сообщения](#method-editmessage)     |
| &#10004; | **DELETE** /messages            | [Удалить сообщение](#method-deletemessage)          |
| &#10004; | **GET** /messages/{_messageId_} | [Получить сообщение](#method-getmessage)            |
| &#10004; | **GET** /videos/{_videoToken_}  | [Получить информацию о видео](#method-getvideoinfo) |
| &#10004; | **POST** /answers               | [Ответ на Callback](#method-answercallback)         |

### chats
|          | метод                                       | описание                                                                           |
|----------|---------------------------------------------|------------------------------------------------------------------------------------|
| &#10004; | **GET** /chats                              | [Получение списка всех групповых чатов](#method-getchats)                          |
| &#10004; | **GET** /chats/{_chatId_}                   | [Получение информации о групповом чате](#method-getchat)                           |
| &#10004; | **PATCH** /chats/{_chatId_}                 | [Изменение информации о групповом чате](#method-editchatinfo)                      |
| &#10004; | **DELETE** /chats/{_chatId_}                | [Удаление группового чата](#method-deletechat)                                     |                
| &#10004; | **POST** /chats/{_chatId_}/actions          | [Отправка действия в групповой чат](#method-sendchataction)                        |          
| &#10004; | **GET** /chats/{_chatId_}/pin               | [Получение закрепленного сообщения в групповом чате](#method-getchatpinnedmessage) |        
| &#10004; | **PUT** /chats/{_chatId_}/pin               | [Закрепление сообщения в групповом чате](#method-pinmessage)                       |      
| &#10004; | **DELETE** /chats/{_chatId_}/pin            | [Удаление закрепленного сообщения в групповом чате](#method-unpinmessage)          |    
| &#10004; | **GET** /chats/{_chatId_}/members/me        | [Получение информации о членстве бота в групповом чате](#method-getchatmyinfo)     |  
| &#10004; | **DELETE** /chats/{_chatId_}/members/me     | [Удаление бота из группового чата](#method-leavechat)                              |
| &#10004; | **GET** /chats/{_chatId_}/members/admins    | [Получение списка администраторов группового чата](#method-getchatadmins)          |
| &#10004; | **POST** /chats/{_chatId_}/members/admins   | [Назначить администратора группового чата](#method-addchatadmins)                  |
| &#10004; | **DELETE** /chats/{_chatId_}/members/admins | [Отменить права администратора группового чата](#method-deletechatadmin)           |
| &#10004; | **GET** /chats/{_chatId_}/members           | [Получение участников группового чата](#method-getchatmembers)                     |
| &#10004; | **POST** /chats/{_chatId_}/members          | [Добавление участников в групповой чат](#method-inviteuser)                        |
| &#10004; | **DELETE** /chats/{_chatId_}/members        | [Удаление участников из группового чата](#method-kickuser)                         |

### upload
|          | метод             | описание                              |
|----------|-------------------|---------------------------------------|
| &#10004; | **POST** /uploads | [Загрузка файлов](#method-uploadfile) |

### subscriptions
|          | метод                     | описание                                       |
|----------|---------------------------|------------------------------------------------|
| &#10004; | **GET** /subscriptions    | [Получение подписок](#method-getwebhookinfo)   |
| &#10004; | **POST** /subscriptions   | [Подписка на обновления](#method-setwebhook)   |
| &#10004; | **DELETE** /subscriptions | [Отписка от обновлений](#method-deletewebhook) |
| &#10004; | **GET** /updates          | [Получение обновлений](#method-getupdates)     |

### [Список изменений](#changelog) 
### [Документация](#documentation) 
### [Модели данных](#datamodels)
### [Перечисления типов](#enums)
### [Пример для minimal api - webhook](#example)
### [Пример консольного приложения - long-polling](#example2)

### добавлен проект-пример SampleBot
проект реализует сценарий бота уведомлений для событий из Zabbix.

---
## Список изменений
<a id="changelog"></a>

---
## изменения 1.0.12
+ добавлена перегрузка для метода AddButton(string text), добавляющая MessageButton в InlineKeyboard

```csharp
InlineKeyboard AddButton(string text)
```

## изменения 1.0.11
+ добавлен опциональный параметр Stream в метод UploadFile, для возможности работы с программно-подготовленным контентом вместо чтения с диска.
```csharp
async Task<UploadDataResponse> UploadFile(UploadType type, string filename, Stream? fileStream = null)
```



## изменения 1.0.10
+ Добавлен механизм повторных запросов при отправке сообщения, вложения которого требуют время на обработку платформой. Обработка ошибки "Key: errors.process.attachment.file.not.processed".
  При возникновении ошибки "errors.process.attachment.file.not.processed", будет произведена повторная попытка отправки сообщения. Задержка перед повторной попыткой расчитывается по формуле [номер попытки * 5 сек], за количество попыток в **MaxBotClientOptions** отвечает
  **RetryWaitAttachment** (значение по умолчанию - **10**, итого, максимум времени ожидания - до 275 сек), после этого исключение будет выброшено во внешнюю обработку.

## изменения 1.0.9
+ [FIX] не обрабатывались апдейты типа used_added/user_removed

## изменения 1.0.8
+ добавлен метод расширения **ReplyMessage**


## изменения 1.0.7
+ добавлена поддержка **long-polling** (из документации: Long Polling — для разработки и тестирования, только Webhook — для production-окружения)
+ добавлен метод AnswerCallback идентичный SendCallbackReact, более привычный для тех кто переносит код с телеграм-бота

Для работы long-polling достаточно задать обработчики событий OnError и OnUpdate, опционально можно задать обработчик OnMessage (без него, события типа MessageCreated и MessageEdited будут обрабатываться в OnUpdate) и поддерживать рабочий цикл ПО до завершения.




## Изменения 1.0.6
+ Добавлены расширения для InlineKeyboard, упрощающие работу с ней.


```csharp
public InlineKeyboard AddButton(string text, string callbackPayload)
```
добавляет CallbackButton в клавиатуру

```csharp
public InlineKeyboard AddButton(Button button)
```
добавляет Button в клавиатуру (для иных типов кнопок)

```csharp
public InlineKeyboard AddNewRow(params Button[] buttons)
```
добавляет новую строку в клавиатуру (с кнопками или без)

#### Пример использования
```csharp
InlineKeyboard ik = new ();
ik.AddButton("Кнопка 1 строка 1", "callback payload 1");
ik.AddButton("Кнопка 2 строка 1", "callback payload 2");
ik.AddNewRow();
ik.AddButton("Кнопка 3 строка 2", "callback payload 3");
ik.AddNewRow().AddButton("Кнопка 4 строка 3", "callback payload 4").AddButton("Кнопка 5 строка 3", "callback payload 5");

NewMessageBody nm = new NewMessageBody()
{
    Attachments = [(InlineKeyboardAttachmentRequest)ik],
    Text = "Нажмите на любую кнопку"
};

var apiResponse = await maxbot.SendCallbackReact(callback_id, nm);
```
---
## Документация
<a id="documentation"></a>

---
### Методы:

### subscriptions

#### Подписка на события (регистрация webhook)
<a id="method-setwebhook"></a>
```csharp
async Task<ApiRespone> SetWebhook(string url, string? secretToken = null, IEnumerable<UpdateType>? updateTypes = null)
```

Подписывает бота на получение обновлений через WebHook. После вызова этого метода бот будет получать уведомления о новых событиях в чатах на указанный URL. Ваш
сервер должен прослушивать один из следующих портов: 80, 8080, 443, 8443, 16384-32383

+ **url** (string) URL HTTP(S)-эндпойнта вашего бота. Должен начинаться с http(s)://
+ **secret** (string?) от 5 до 256 символов. Cекрет, который должен быть отправлен в заголовке X-Max-Bot-Api-Secret в каждом запросе Webhook. Разрешены только
  символы A-Z, a-z, 0-9, и дефис. Заголовок рекомендован, чтобы запрос поступал из установленного веб-узла.
+ **updateTypes** (IEnumerable<[UpdateType](#enum-updatetype)>?) - Список типов обновлений, которые ваш бот хочет получать. Если передается null - бот будет получать все возможные
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

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Удаление подписки на события
<a id="method-deletewebhook"></a>
```csharp
async Task<ApiResponse> DeleteWebhook(string url)
```

Отписывает бота от получения обновлений через Webhook. После вызова этого метода бот перестаёт получать уведомления о новых событиях, и становится доступна
доставка уведомлений через API с длительным опросом

+ **url** (string) URL, который нужно удалить из подписок на WebHook.

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получение подписок
Запрос действующих подписок

<a id="method-getwebhookinfo"></a>
```csharp
async Task<Subscriptions> GetWebhookInfo()
```

Если ваш бот получает данные через Webhook, этот метод возвращает список всех подписок. При настройке уведомлений для production-окружения рекомендуем
использовать Webhook.

_Обратите внимание: для отправки вебхуков поддерживается только протокол HTTPS, включая самоподписанные сертификаты. HTTP не поддерживается_

Возвращает объект [**Subscriptions**](#model-subscriptions) - список текущих  подписок


#### Получение обновлений
<a id="method-getupdates"></a>
_применяется при long-polling подключении_
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
+ **types** (IEnumerable<[UpdateType](#enum-updatetype)>?) Список типов обновлений, которые ваш бот хочет получать. Если передается null - бот будет получать все возможные
  обновления

Возвращает объект [**UpdatesResponse**](#model-updatesresponse) содержащий массив [Update](#model-update)

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

#### Получение информации о боте
<a id="method-getme"></a>
```csharp
async Task<BotInfo> GetMe()
```

Метод возвращает информацию о боте, который идентифицируется с помощью токена доступа access_token. В ответе приходит объект [User](#model-user) с вариантом наследования
[BotInfo](#model-botinfo), который содержит идентификатор бота, его название, никнейм, время последней активности, описание и аватар (при наличии)


#### Изменение информации о боте
<a id="method-editme"></a>
```csharp

async Task<ApiResponse> EditMe(string? name = null, string? description = null, 
    IEnumerable<BotCommand>? commands = null, PhotoAttachmentRequestPayload? photo = null) 
```
+ **name** (string?) - отображаемое имя бота
+ **description** (string?) - описание бота
+ **commands** (IEnumerable<[BotCommand](#model-botcommand)>?) - перечень доступных комманд бота
+ **photo** ([PhotoAttachmentRequestPayload](#model-photoattachmentrequestpayload?)) - изображение для профиля бота

#### !Внимание! Данный метод не задокументирован в официальной документации и взят из исходников библиотек под TS. Использовать на свой страх и риск!

Возвращает объект [**ApiResponse**](#model-apiresponse)
### messages

#### Получение сообщений
<a id="method-getmessages"></a>
```csharp
async Task<MessagesResponse> GetMessages(long chat_id, DateTime? from = null, DateTime? to = null, int? count = null)
async Task<MessagesResponse> GetMessages(IEnumerable<string> messages_ids, DateTime? from = null, DateTime? to = null, int? count = null)
```

Метод возвращает информацию о сообщении или массив сообщений из чата. Для выполнения запроса нужно указать один из параметров — chat_id или message_ids:

+ **chat_id** (long) — метод возвращает массив сообщений из указанного чата. Сообщения возвращаются в обратном порядке: последние сообщения будут первыми в
  массиве.
  ID чата, чтобы получить сообщения из определённого чата. Обязательный параметр, если не указан message_ids


+ **message_ids** (IEnumerable<**string**>) — метод возвращает информацию о запрошенных сообщениях. Можно указать один идентификатор или несколько. Список ID
  сообщений, которые нужно получить (через запятую). Обязательный параметр, если не указан chat_id

+ **from** (DateTime?) - Время начала для запрашиваемых сообщений
+ **to** (DateTime?) - Время окончания для запрашиваемых сообщений
+ **count** (int?) - Максимальное количество сообщений в ответе, по умолчанию 50

Возвращает объект [**MessagesResponse**](#model-messagesresponse)

#### Отправить сообщение
<a id="method-sendmessage"></a>
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
+ **attachments** (IEnumerable<[AttachmentRequest](#model-attachmentrequest)>?) - Вложения сообщения. Если пусто, все вложения будут удалены
+ **link** ([NewMessageLink](#model-newmessagelink)?) - Ссылка на сообщение (для ответов и репостов)
+ **notify** (bool) - Если false, участники чата не будут уведомлены (по умолчанию true)
+ **text_format** ([TextFormat](#enum-textformat)?) - Если установлен, текст сообщения будет форматирован данным способом, возможные варианты **Markdown** или **HTML**

Возвращает объект [**ApiMessage**](#model-apimessage)

#### Ответ на сообщение (вспомогательный метод)
```csharp
async Task<ApiMessage> ReplyMessage(Message message, string? text, bool disable_link_preview = false,
            bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumerable<AttachmentRequest>? attachments = null)
```
Данный метод отвечает на входящее сообщение, автоматически выбирая SendMessage или SendMessageToChat

Возвращает объект [**ApiMessage**](#model-apimessage)


#### Редактирование сообщения
<a id="method-editmessage"></a>
```csharp
async Task<ApiResponse> EditMessage(string message_id, string? text = null, IEnumberable<AttachmentRequest>? attachments = null, NewMessageLink? link = null,
            bool notify = true, TextFormat text_format = TextFormat.HTML)
```

Редактирует сообщение в чате. Если поле attachments равно null, вложения текущего сообщения не изменяются. Если в этом поле передан пустой список, все вложения
будут удалены

_С помощью метода можно отредактировать сообщения, которые отправлены менее 24 часов назад_

+ **message_id** (string) - ID редактируемого сообщения
+ **text** (string?) - Новый текст сообщения, до 4000 символов
+ **attachments** (IEnumerable<[AttachmentRequest](#model-attachmentrequest)>) - Вложения сообщения. Если пусто, все вложения будут удалены
+ **link** ([NewMessageLink](#model-newmessagelink)?) - Ссылка на сообщение (для ответов и репостов)
+ **notify** (bool) - Если false, участники чата не будут уведомлены (по умолчанию true)
+ **text_format** ([TextFormat](#enum-textformat)?) - Если установлен, текст сообщения будет форматирован данным способом, возможные варианты **Markdown** или **HTML**

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Удалить сообщение
<a id="method-deletemessage"></a>
```csharp
async Task<ApiResponse> DeleteMessage(string messageId)
```

Удаляет сообщение в диалоге или чате, если бот имеет разрешение на удаление сообщений

_С помощью метода можно удалять сообщения, которые отправлены менее 24 часов назад_

+ **message_id** (string) - ID удаляемого сообщения

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Ответ на Callback
<a id="method-answercallback"></a>
```csharp
async Task<ApiResponse> SendCallbackReact(string callback_id, NewMessageBody? newMessageBody = null, string? notification = null)
// дополнительное название одного и того же метода 
async Task<ApiResponse> AnswerCallback(string callback_id, NewMessageBody? newMessageBody = null, string? notification = null)    
```

Этот метод используется для отправки ответа после того, как пользователь нажал на кнопку. Ответом может быть обновленное сообщение и/или одноразовое уведомление
для пользователя

+ **callback_id** (string) - Идентификатор кнопки, по которой пользователь кликнул. Бот получает идентификатор как часть Update с типом **MessageCallbackUpdate
  **.
+ **newMessageBody** ([NewMessageBody](#model-newmessagebody)?) - Заполните это, если хотите изменить текущее сообщение (текст, вложения, кнопки)
+ **notification** (string?) - Заполните это, если хотите просто отправить одноразовое уведомление пользователю (не отображается в десктоп-клиенте, возможно
  временно, но есть в мобильных приложениях)

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получить сообщение
<a id="method-getmessage"></a>
```csharp
async Task<Message> GetMessage(string message_id)
```

Возвращает сообщение по его ID

+ **message_id** (string) - ID сообщения (mid), чтобы получить одно сообщение в чате

Возвращает объект [**Message**](#model-message)

#### Получить информацию о видео
<a id="method-getvideoinfo"></a>
```csharp
async Task<VideoInfo> GetVideoInfo(string video_token)                        
```

Возвращает подробную информацию о прикреплённом видео. URL-адреса воспроизведения и дополнительные метаданные

+ **video_token** (string) Токен видео-вложения

Возвращает объект [**VideoInfo**](#model-videoinfo)

### chat

#### Получение списка всех групповых чатов
<a id="method-getchats"></a>
```csharp
async Task<ChatsResponse> GetChats()
async Task<ChatsResponse> GetChats(int count = 50, long? marker = null)
```

Возвращает список групповых чатов, в которых участвовал бот, информацию о каждом чате и маркер для перехода к следующей странице списка

+ **count** (int) - Количество запрашиваемых чатов, по умолчанию 50
+ **marker** (long?) - Указатель на следующую страницу данных. Для первой страницы передайте null

Возвращает объект [**ChatsResponse**](#model-chatsresponse)

#### Получение информации о групповом чате
<a id="method-getchat"></a>
```csharp
async Task<Chat> GetChat(long chat_id)
```

Возвращает информацию о групповом чате по его ID

+ **chat_id** (long) - ID запрашиваемого чата

Возвращает объект [**Chat**](#model-chat)

#### Изменение информации о групповом чате
<a id="method-editchatinfo"></a>
```csharp
async Task<Chat> EditChatInfo(long chat_id, PhotoAttachmentRequestPayload? icon = null, string? title = null, string? pin = null,
            bool? notify = null)
```

Позволяет редактировать информацию о групповом чате, включая название, иконку и закреплённое сообщение

+ **chat_id** (long) - ID редактируемого чата
+ **icon** ([PhotoAttachmentRequestPayload](#model-photoattachmentrequestpayload)?) - Запрос на прикрепление изображения (все поля являются взаимоисключающими)
+ **title** (string?) - от 1 до 200 символов
+ **pin** (string?) - ID сообщения для закрепления в чате. Чтобы удалить закреплённое сообщение, используйте метод **UnpinMessage**
+ **notify** (bool?) - Если true, участники получат системное уведомление об изменении

Возвращает объект [**Chat**](#model-chat)

#### Удаление группового чата
<a id="method-deletechat"></a>
```csharp
async Task<ApiResponse>DeleteChat(long chat_id)
```

Удаляет групповой чат для всех участников

+ **chat_id** (long) - ID удаляемого чата

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Отправка действия бота в групповой чат
<a id="method-sendchataction"></a>
```csharp
async Task<ApiResponse> SendChatAction(long chat_id, SenderAction action)
```

Позволяет отправлять в групповой чат такие действия бота, как например: «набор текста» или «отправка фото»

+ **chat_id** (long) - ID группового чата
+ **action** ([SenderAction](#enum-senderaction)) - Действие, отправляемое участникам чата. Возможные значения:
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

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получение закреплённого сообщения в групповом чате
<a id="method-getchatpinnedmessage"></a>
```csharp
async Task<ApiMessage> GetChatPinnedMessage(long chat_id)
```

Возвращает закреплённое сообщение в групповом чате

+ **chat_id** (long) - ID группового чата

Возвращает объект [**ApiMessage**](#model-apimessage)

#### Закрепление сообщения в групповом чате
<a id="method-pinmessage"></a>
```csharp
async Task<ApiResponse> PinMessage(long chat_id, string message_id, bool notify = true)
```

+ **chat_id** (long) - ID группового чата
+ **message_id** (string) - ID сообщения, которое нужно закрепить. Соответствует полю [Message](#model-message).[MessageBody](#model-messagebody).MessageId
+ **notify** (bool) - Если true, участники получат уведомление с системным сообщением о закреплении

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Удаление закреплённого сообщения в групповом чате
<a id="method-unpinmessage"></a>
```csharp
async Task<ApiResponse> UnpinMessage(long chat_id)
```

Удаляет закреплённое сообщение в групповом чате

+ **chat_id** (long) - ID группового чата

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получение информации о членстве бота в групповом чате
<a id="method-getchatmyinfo"></a>
```csharp
async Task<ChatMember> GetChatMyInfo(long chat_id)
```

Возвращает информацию о членстве текущего бота в групповом чате. Бот идентифицируется с помощью токена доступа

+ **chat_id** (long) - ID группового чата

Возвращает наследованный от [**User**](#model-user) объект [**ChatMember**](#model-chatmember)

#### Удаление бота из группового чата
<a id="method-leavechat"></a>
```csharp
async Task<ApiResponse> LeaveChat(long chat_id)
```

Удаляет бота из участников группового чата

+ **chat_id** (long) - ID группового чата

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получение списка администраторов группового чата
<a id="method-getchatadmins"></a>
```csharp
async Task<ChatMembersResponse> GetChatAdmins(long chat_id, long? marker = null)
```

Возвращает список всех администраторов группового чата. Бот должен быть администратором в запрашиваемом чате

+ **chat_id** (long) - ID группового чата
+ **marker** (long?) - Указатель на следующую страницу данных

Возвращает объект [**ChatMembersResponse**](#model-chatmembersresponse)

#### Назначить администратора группового чата
<a id="method-addchatadmins"></a>
```csharp
async Task<ApiResponse> AddChatAdmins(long chat_id, IEnumerable<ChatAdmin> admins)
```

Возвращает значение true, если в групповой чат добавлены все администраторы.

_В группе может быть не более 50 администраторов_

+ **chat_id** (long) - ID группового чата
+ **admins** (IEnumerable<[ChatAdmin](#model-chatadmin)>) - Список пользователей, которые получат права администратора чата

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Отменить права администратора в групповом чате
<a id="method-deletechatadmin"></a>
```csharp
async Task<ApiResponse> DeleteChatAdmin(long chat_id, long user_id)
```

Отменяет права администратора у пользователя в групповом чате, лишая его административных привилегий

+ **chat_id** (long) - ID группового чата
+ **user_id** (long) - ID пользователя

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Получение участников группового чата
<a id="method-getchatmembers"></a>
```csharp
async Task<ChatMembersResponse> GetChatMembers(long chat_id)
async Task<ChatMembersResponse> GetChatMembers(long chat_id, long marker, int count = 20)
async Task<ChatMembersResponse> GetChatMembers(long chat_id, IEnumerable<long> user_ids)
```

Возвращает список участников группового чата

+ **chat_id** (long) - ID группового чата
+ **user_ids** (IEnumerable<**long**>) - Список ID пользователей, чье членство нужно получить. Когда этот параметр передан, параметры count и marker игнорируются
+ **marker** (long) - Указатель на следующую страницу данных
+ **count** (int) - Количество участников, которых нужно вернуть, по умолчанию 20

Возвращает объект [**ChatMembersReponse**](#model-chatmembersresponse)

#### Добавление участников в групповой чат
<a id="method-inviteuser"></a>
```csharp
async Task<ApiResponse> InviteUser(long chat_id, IEnumerable<long> user_ids)
```

Добавляет участников в групповой чат. Для этого могут потребоваться дополнительные права

+ **chat_id** (long) - ID группового чата
+ **user_ids** (IEnumerable<**long**>) - Список ID пользователей для добавления в чат

Возвращает объект [**ApiResponse**](#model-apiresponse)

#### Удаление участника из группового чата
<a id="method-kickuser"></a>
```csharp
async Task<ApiResponse> KickUser(long chat_id, long user_id, bool block = false)            
```

Удаляет участника из группового чата. Для этого могут потребоваться дополнительные права

+ **chat_id** (long) - ID группового чата
+ **user_id** (long) - ID пользователя, которого нужно удалить из чата
+ **block** (bool) - Если установлено в true, пользователь будет заблокирован в чате. Применяется только для чатов с публичной или приватной ссылкой.
  Игнорируется в остальных случаях

Возвращает объект [**ApiResponse**](#model-apiresponse)

### Upload
#### Загрузка файлов
<a id="method-uploadfile"></a>
```csharp
async Task<UploadDataResponse> UploadFile(UploadType type, string filename, Stream? fileStream = null)       
```
Делает полную операцию по загрузке файла: получет ссылку на загрузку и по ней загружает файл
+ **type** ([UploadType](#enum-uploadtype)) - тип загружаемого файла, может быть Image, Video, Audio, File
+ **filename** (string) - путь до загружаемого файла
+ **fileStream** (Stream?) - опциональный параметр для загрузки файлов с помощью Stream (с 1.0.11)

#### Типы файлов:
+ + image: JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
+ + video: MP4, MOV, MKV, WEBM, MATROSKA
+ + audio: MP3, WAV, M4A и другие
+ + file: любые типы файлов


Может вызывать исключение типа **FileNotFoundException**.
Возвращает объект [**UploadDataResponse**](#model-uploaddataresponse)

---
<a id="datamodels"></a>
## Модели данных

<a id="model-apimessage"></a>
#### ApiMessage
```csharp
public class ApiMessage
{
    public required Message Message { get; set; }
}
```

#### ApiResponse
<a id="model-apiresponse"></a>
```csharp
public class ApiResponse
{
    // true, если запрос был успешным, false в противном случае
    public bool Success { get; set; }

    // Объяснительное сообщение, если результат не был успешным
    public string? Message { get; set; }
}
```

<a id="model-attachment"></a>
#### Attachment
[ContactAttachemnt](#model-contactattachment) | 
[InlineKeyboardAttachment](#model-inlinekeyboardattachment) |
[StickerAttachment](#model-stickerattachment) |
[FileAttachment](#model-fileattachment) |
[AudioAttachment](#model-audioattachment) |
[VideoAttachment](#model-videoattachment) |
[ShareAttachment](#model-shareattachment) |
[ImageAttachment](#model-imageattachment) |
[LocationAttachment](#model-locationattachment)

```csharp
public abstract class Attachment
{
    // image, video, audio, file, sticker, contact, inline_keyboard, share, location 
    public abstract AttachmentType Type { get; set; }
}
```
<a id="model-contactattachment"></a>
- **ContactAttachment**
```csharp
public class ContactAttachment : Attachment
{
    public required ContactAttachmentPayload Payload { get; set; }    
}
```
<a id="model-inlinekeyboardattachment"></a>
- **InlineKeyboardAttachment**
```csharp
public class InlineKeyboardAttachment : Attachment
{
    public required InlineKeyboard Payload { get; set; }
}
```
<a id="model-stickerattachment"></a>
- **StickerAttachment**
```csharp
public class StickerAttachment : Attachment
{
    public required StickerAttachmentPayload Payload { get; set; }

    // Ширина стикера
    public int Width { get; set; }

    // Высота стикера
    public int Height { get; set; }
}
```
<a id="model-fileattachment"></a>
- **FileAttachment**
```csharp
public class FileAttachment : Attachment
{
    public required FileAttachmentPayload Payload { get; set; }

    // Имя загруженного файла
    public required string Filename { get; set; }

    // Размер файла в байтах
    public long Size { get; set; }
}
```
<a id="model-audioattachment"></a>
- **AudioAttachment**
```csharp
public class AudioAttachment : Attachment
{
    public required MediaAttachmentPayload Payload { get; set; }

    // Аудио транскрипция
    public string? Transcription { get; set; }
}
```
<a id="model-videoattachment"></a>
- **VideoAttachment**
```csharp
public class VideoAttachment : Attachment
{
    public required MediaAttachmentPayload Payload { get; set; }

    // Миниатюра видео
    public VideoThumbnail? Thumbnail { get; set; }

    // Ширина видео
    public int? Width { get; set; }

    // Высота видео
    public int? Height { get; set; }

    // Длина видео в секундах
    public int? Duration { get; set; }
}
```
<a id="model-shareattachment"></a>
- **ShareAttachment**
```csharp
public class ShareAttachment : Attachment
{
    public required ShareAttachmentPayload Payload { get; set; }

    // Заголовок предпросмотра ссылки.
    public string? Title { get; set; }

    // Описание предпросмотра ссылки
    public string? Description { get; set; }

    // Изображение предпросмотра ссылки
    public string? ImageUrl { get; set; }
}
```
<a id="model-imageattachment"></a>
- **ImageAttachment**
```csharp
public class ImageAttachment : Attachment
{
    public required PhotoAttachmentPayload Payload { get; set; }
}
```
<a id="model-locationattachment"></a>
- **LocationAttachment**
```csharp

public class LocationAttachment : Attachment
{
    // широта
    public double Latitude { get; set; }

    // долгота
    public double Longitude { get; set; }
}
```
<a id="model-attachmentrequest"></a>
#### AttachmentRequest
[ImageAttachmentRequest](#model-imageattachmentrequest) |
[VideoAttachmentRequest](#model-videoattachmentrequest) |
[AudioAttachmentRequest](#model-audioattachmentrequest) |
[FileAttachmentRequest](#model-fileattachmentrequest) |
[StickerAttachmentRequest](#model-stickerattachmentrequest) |
[ContactAttachmentRequest](#model-contactattachmentrequest) |
[InlineKeyboardAttachmentRequest](#model-inlinekeyboardattachmentrequest) |
[LocationAttachmentRequest](#model-locationattachmentrequest) |
[ShareAttachmentRequest](#model-shareattachmentrequest)
```csharp
public abstract class AttachmentRequest
{
    // Тип
    public abstract AttachmentType Type { get; }    
}
```
<a id="model-imageattachmentrequest"></a>
- **ImageAttachmentRequest**
```csharp
public class ImageAttachmentRequest : AttachmentRequest
{
    // Запрос на прикрепление изображения (все поля являются взаимоисключающими)
    public required PhotoAttachmentRequestPayload  Payload { get; set; }    
}
```
<a id="model-videoattachmentrequest"></a>
- **VideoAttachmentRequest**
```csharp
public class VideoAttachmentRequest : AttachmentRequest
{
    // Это информация, которую вы получите, как только аудио/видео будет загружено
    public required UploadedInfo Payload { get; set; }
} 
```
<a id="model-audioattachmentrequest"></a>
- **AudioAttachmentRequest**
```csharp
public class AudioAttachmentRequest : AttachmentRequest
{
    // Это информация, которую вы получите, как только аудио/видео будет загружено
    public required UploadedInfo Payload { get; set; }
}
```
<a id="model-fileattachmentrequest"></a>
- **FileAttachmentRequest**
```csharp
public class FileAttachmentRequest : AttachmentRequest
{
    // Это информация, которую вы получите, как только аудио/видео будет загружено
    public required UploadedInfo Payload { get; set; }
}
```
<a id="model-stickerattachmentrequest"></a>
- **StickerAttachmentRequest**
```csharp
public class StickerAttachmentRequest : AttachmentRequest
{
    public required StickerAttachmentRequestPayload Payload { get; set; }
}
```
<a id="model-contactattachmentrequest"></a>
- **ContactAttachmentRequest**
```csharp
public class ContactAttachmentRequest : AttachmentRequest
{
    public required ContactAttachmentRequestPayload Payload { get; set; }
}
```
<a id="model-inlinekeyboardattachmentrequest"></a>
- **InlineKeyboardAttachmentRequest**
```csharp
public class InlineKeyboardAttachmentRequest : AttachmentRequest
{
    public required InlineKeyboardAttachmentRequestPayload Payload { get; set; }
}
```
<a id="model-locationattachmentrequest"></a>
- **LocationAttachmentRequest**
```csharp
public class LocationAttachmentRequest : AttachmentRequest
{
    // широта
    public double Latitude { get; set; }
    
    // долгота
    public double Longitude { get; set; }
}
```
<a id="model-shareattachmentrequest"></a>
- **ShareAttachmentRequest**
```csharp
public class ShareAttachmentRequest : AttachmentRequest
{
    public required ShareAttachmentPayload Payload { get; set; }
}

```
<a id="model-botcommand"></a>
#### BotCommand
```csharp
public class BotCommand
{
    // от 1 до 64 символов, Название команды
    public required string Name { get; set; }

    // от 1 до 128 символов, Описание команды (по желанию)
    public string? Description { get; set; }

}
```
<a id="model-botinfo"></a>
#### BotInfo
```csharp
public class BotInfo : User 
{
    // до 16000 символов. Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
    public string? Description { get; set; }
    // URL аватара пользователя или бота в уменьшенном размере
    public string? AvatarUrl { get; set; }
    // URL аватара пользователя или бота в полном размере
    public string? FullAvatarUrl { get; set; }
    // Команды, поддерживаемые ботом. до 32 элементов
    public BotCommand[]? Commands { get; set; }
}    
```
<a id="model-button"></a>
### Button
[MessageButton](#model-messagebutton) | 
[OpenAppButton](#model-openappbutton) |
[RequestContactButton](#model-requestcontactbutton) |
[RequestGeoLocationButton](#model-requestgeolocationbutton) |
[LinkButton](#model-linkbutton) |
[CallbackButton](#model-callbackbutton)
```csharp
public abstract class Button
{
    public abstract ButtonType Type { get; }
}
```
<a id="model-messagebutton"></a>
- **MessageButton**
```csharp
public class MessageButton : Button
{
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }
}
```
<a id="model-openappbutton"></a>
- **OpenAppButton**
```csharp
public class OpenAppButton : Button
{
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }

    // Публичное имя (username) бота или ссылка на него, чьё мини-приложение надо запустить
    public string WebApp { get; set; } = string.Empty;

    // Идентификатор бота, чьё мини-приложение надо запустить
    public long ContactId { get; set; }

    // Параметр запуска, который будет передан в initData мини-приложения
    public string Payload { get; set; } = string.Empty;
}
```
<a id="model-requestcontactbutton"></a>
- **RequestContactButton**
```csharp
public class RequestContactButton : Button
{
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }
}
```
<a id="model-requestgeolocationbutton"></a>
- **RequestGeoLocationButton**
```csharp
public class RequestGeoLocationButton : Button
{    
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }

    // Если true, отправляет местоположение без запроса подтверждения пользователя
    public bool Quick { get; set; }
}
```
<a id="model-linkbutton"></a>
- **LinkButton**
```csharp
public class LinkButton : Button
{
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }

    // до 2048 символов
    public required string Url { get; set; }
}
```
<a id="model-callbackbutton"></a>
- **CallbackButton**
```csharp
public class CallbackButton : Button
{
    // от 1 до 128 символов
    // Видимый текст кнопки
    public required string Text { get; set; }

    // до 1024 символов
    // Токен кнопки
    public required string Payload { get; set; }
}
```
<a id="model-callback"></a>
#### Callback
```csharp
public class Callback
{
    // время, когда пользователь нажал кнопку
    public DateTime TimeStamp { get; set; }
    
    // Текущий ID клавиатуры
    public required string CallbackId { get; set; }
    
    // Токен кнопки
    public string? Payload { get; set; }
    
    // Пользователь, нажавший на кнопку
    public required User User { get; set; }

}
```
<a id="model-chat"></a>
#### Chat
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

<a id="model-chatadmin"></a>
#### ChatAdmin
```csharp
public class ChatAdmin
{
    // Идентификатор пользователя-участника чата, который назначается администратором
    // Максимум — 50 администраторов в чате
    public long UserID { get; set; }

    // Перечень прав доступа пользователя. Возможные значения:
    // "read_all_messages" — Читать все сообщения. Это право важно при назначении ботов: без него бот не будет получать апдейты (вебхуки) в групповом чате
    // "add_remove_members" — Добавлять/удалять участников
    // "add_admins" — Добавлять администраторов
    // "change_chat_info" — Изменять информацию о чате
    // "pin_message" — Закреплять сообщения
    // "write" — Писать сообщения
    // "can_call" — Совершать звонки
    // "edit_link" — Изменять ссылку на чат
    // "post_edit_delete_message" — Публиковать, редактировать и удалять сообщения
    // "edit_message" — Редактировать сообщения
    // "delete_message" — Удалять сообщения
    // Если право назначается администратору, то обновляются его текущие права доступа
    public required IEnumerable<ChatAdminPermission> Permissions { get; set; }

    // Титул администратора (вместо "админ" и "владелец")
    public string? Alias { get; set; }

}
```
<a id="model-chatmember"></a>
#### ChatMember
```csharp
public class ChatMember:User
{
    // до 16000 символов
    // Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
    public string? Description { get; set; }

    // URL аватара пользователя или бота в уменьшенном размере
    public string? AvatarUrl { get; set; }

    // URL аватара пользователя или бота в полном размере
    public string? FullAvatarUrl { get; set; }

    // Время последней активности пользователя в чате. Может быть устаревшим для суперчатов (равно времени вступления)
    public DateTime LastAccessTime { get; set; }
    
    // Является ли пользователь владельцем чата
    public bool IsOwner { get; set; }
    
    // Является ли пользователь администратором чата 
    public bool IsAdmin { get; set; }

    // Дата присоединения к чату в формате Unix time
    public DateTime JoinTime { get; set; }
    
    // Перечень прав пользователя. Возможные значения:
    // "read_all_messages" — Читать все сообщения.
    // "add_remove_members" — Добавлять/удалять участников.
    // "add_admins" — Добавлять администраторов.
    // "change_chat_info" — Изменять информацию о чате.
    // "pin_message" — Закреплять сообщения.
    // "write" — Писать сообщения.
    // "edit_link" — Изменять ссылку на чат.
    public ChatAdminPermission[]? Permissions { get; set; }
    
    // Заголовок, который будет показан на клиенте
    // Если пользователь администратор или владелец и ему не установлено это название, то поле не передаётся, клиенты на своей стороне подменят на "владелец" или "админ"
    public string? Alias { get; set; }    
}
```
<a id="model-chatmembersresponse"></a>
#### ChatMembersResponse
```csharp
public class ChatMembersResponse
{
    // Список участников чата с информацией о времени последней активности
    public required ChatMember[] ChatMembers { get; set; }
    
    // Указатель на следующую страницу данных
    public long? Marker  { get; set; }    
}
```

<a id="model-chatsresponse"></a>
#### ChatsResponse
```csharp
public class ChatsResponse
{
    public required IEnumerable<Chat> Chats { get; set; }
}
```

<a id="model-inlinekeyboard"></a>
#### InlineKeyboard
```csharp
public class InlineKeyboard
{
    // двумерный массив кнопок ([строка][столбец])
    public required IEnumerable<IEnumerable<Button>> Buttons { get; set; } = [];   

}

```
<a id="model-linkedmessage"></a>
#### LinkedMessage
```csharp
public class LinkedMessage
{
    // Тип связанного сообщения
    public MessageLinkType Type { get; set; }
    
    // Пользователь, отправивший сообщение.
    public User? Sender { get; set; }
    
    // Чат, в котором сообщение было изначально опубликовано. Только для пересланных сообщений
    public long? ChatId { get; set; }
    
    // Схема, представляющая тело сообщения
    public required MessageBody MessageBody { get; set; }

}
```
<a id="model-markupelement"></a>
#### MarkupElement
```csharp
public abstract class MarkupElement
{
    // Тип элемента разметки. Может быть жирный, курсив, зачеркнутый, подчеркнутый, моноширинный, ссылка или упоминание пользователя
    // strong , emphasized, monospaced, link, strikethrough, underline, user_mention
    public abstract MarkupElementType Type { get; set; }

    // Индекс начала элемента разметки в тексте. Нумерация с нуля
    public int From { get; set; }

    // Длина элемента разметки
    public int Length { get; set; } 
}
```
<a id="model-strongmarkupelement"></a>
- **StrongMarkupElement**
```csharp
public class StrongMarkupElement : MarkupElement
{
}
```
<a id="model-emphasizedmarkupelement"></a>
- **EmphasizedMarkupElement**
```csharp
public class EmphasizedMarkupElement : MarkupElement
{
}
```
<a id="model-monospacedmarkupelement"></a>
- **MonospacedMarkupElement**
```csharp
public class MonospacedMarkupElement : MarkupElement
{
}
```
<a id="model-linkmarkupelement"></a>
- **LinkMarkupElement**
```csharp
public class LinkMarkupElement : MarkupElement
{
    // URL ссылки, до 1024 символов
    public required string Url { get; set; }
}
```
<a id="model-strikethroughmarkupelement"></a>
- **StrikethroughMarkupElement**
```csharp
public class StrikethroughMarkupElement : MarkupElement
{
}
```
<a id="model-underlinemarkupelement"></a>
- **UnderlineMarkupElement**
```csharp
public class UnderlineMarkupElement : MarkupElement
{    
}
```
<a id="model-usermentionmarkupelement"></a>
- **UserMentionMarkupElement**
```csharp
public class UserMentionMarkupElement : MarkupElement
{
    // @username упомянутого пользователя
    public string? UserLink { get; set; }

    // ID упомянутого пользователя без имени
    public long? UserID { get; set; }
}
```

<a id="model-message"></a>
#### Message
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

<a id="model-messagebody"></a>
#### MessageBody
```csharp
public class MessageBody
{
    // Уникальный ID сообщения
    public required string MessageId { get; set; }

    // ID последовательности сообщения в чате
    public long SequenceId { get; set; }

    // Новый текст сообщения
    public string? Text { get; set; }

    // Вложения сообщения. Могут быть одним из типов Attachment. Смотрите описание схемы
    public Attachment[]? Attachments { get; set; }

    // Разметка текста сообщения. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0
    public MarkupElement[]? Markup { get; set; }
}
```
<a id="model-messagesresponse"></a>
#### MessagesResponse
```csharp
public class MessagesResponse
{
    // Массив сообщений
    public required Message[]  Messages { get; set; }
}
```

<a id="model-messagestat"></a>
#### MessageStat
```csharp
public class MessageStat
{
    public long Views { get; set; }
}
```
<a id="model-newmessagebody"></a>
#### NewMessageBody
```csharp
public class NewMessageBody
{
    // до 4000 символов
    // Новый текст сообщения
    public string? Text { get; set; }

    // Вложения сообщения. Если пусто, все вложения будут удалены
    public IEnumerable<AttachmentRequest>? Attachments { get; set; }

    // Ссылка на сообщение
    public NewMessageLink? Link { get; set; }

    // По умолчанию: true
    // Если false, участники чата не будут уведомлены (по умолчанию true)
    public bool Notify { get; set; }
    
    // по умолчанию форматирование текста в HTML
    public TextFormat? TextFormat { get; set; }    
}
```
<a id="model-newmessagelink"></a>
#### NewMessageLink
```csharp
public class NewMessageLink
{
    // Тип ссылки сообщения
    public required MessageLinkType Type { get; set; }
    
    // ID сообщения исходного сообщения
    public required string MessageId { get; set; }    
}
```
<a id="model-recipient"></a>
#### Recipient
```csharp
public class Recipient
{
    // ID чата
    public long ChatId { get; set; }

    // Тип чата
    public required ChatType ChatType { get; set; }

    // ID пользователя, если сообщение было отправлено пользователю
    public long? UserId { get; set; }
}
```

<a id="model-subscription"></a>
#### Subscription
```csharp
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

<a id="model-subscriptions"></a>
#### Subscriptions
```csharp
public class Subscriptions 
{
    public Subscription[]? Webhooks { get; set; }
}
```

<a id="model-update"></a>
#### Update
[MessageCreatedUpdate](#model-messagecreatedupdate) |
[MessageCallbackUpdate](#model-messagecallbackupdate) |
[MessageEditedUpdate](#model-messageeditedupdate) |
[MessageRemovedUpdate](#model-messageremovedupdate) |
[BotAddedUpdate](#model-botaddedupdate) |
[BotRemovedUpdate](#model-botremovedupdate) |
[DialogMutedUpdate](#model-dialogmutedupdate) |
[DialogUnmutedUpdate](#model-dialogunmutedupdate) |
[DialogClearedUpdate](#model-dialogclearedupdate) |
[DialogRemovedUpdate](#model-dialogremovedupdate) |
[UserAddedUpdate](#model-useraddedupdate) |
[UserRemovedUpdate](#model-userremovedupdate) |
[BotStartedUpdate](#model-botstartedupdate) |
[BotStoppedUpdate](#model-botstoppedupdate) |
[ChatTitleChangedUpdate](#model-chattitlechangedupdate)
```csharp
public abstract class Update
{
    // ОбъектUpdate представляет различные типы событий, произошедших в чате. См. его наследников
    public  UpdateType UpdateType { get; set; }

    // время, когда произошло событие
    public DateTime TimeStamp { get; set; }

}
```
<a id="model-messagecreatedupdate"></a>
- #### MessageCreatedUpdate
```csharp
public class MessageCreatedUpdate : Update
{
    // Новое созданное сообщение
    public required Message Message { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
 
}
```
<a id="model-messagecallbackupdate"></a>
- #### MessageCallbackUpdate
```csharp
public class MessageCallbackUpdate : Update
{
    // клавиатура
    public required Callback Callback { get; set; }

    // Изначальное сообщение, содержащее встроенную клавиатуру. Может быть null, если оно было удалено к моменту, когда бот получил это обновление
    public Message? Message { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }   
}
```
<a id="model-messageeditedupdate"></a>
- #### MessageEditedUpdate
```csharp
public class MessageEditedUpdate : Update
{
    // Отредактированное сообщение
    public required Message Message { get; set; }
}
```
<a id="model-messageremovedupdate"></a>
- #### MessageRemovedUpdate
```csharp
public class MessageRemovedUpdate : Update
{
    // ID удаленного сообщения
    public required string MessageId { get; set; }

    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, удаливший сообщение
    public long UserId { get; set; }
}
```
<a id="model-botaddedupdate"></a>
- #### BotAddedUpdate
```csharp
public class BotAddedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, добавивший бота в чат
    public required User User { get; set; }

    // Указывает, был ли бот добавлен в канал или нет
    public bool IsChannel { get; set; }
}
```
<a id="model-botremovedupdate"></a>
- #### BotRemovedUpdate
```csharp
public class BotRemovedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, удаливший бота из чата
    public required User User { get; set; }

    // Указывает, был ли бот удалён из канала или нет
    public bool IsChannel { get; set; }
}
```
<a id="model-dialogmutedupdate"></a>
- #### DialogMutedUpdate
```csharp
public class DialogMutedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, который включил уведомления
    public required User User { get; set; }

    // Время в формате Unix, до наступления которого диалог был отключён
    public DateTime MutedUntil { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-dialogunmutedupdate"></a>
- #### DialogUnmutedUpdate
```csharp
public class DialogUnmutedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, который включил уведомления
    public required User User { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-dialogclearedupdate"></a>
- #### DialogClearedUpdate
```csharp
public class DialogClearedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, который включил уведомления
    public required User User { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-dialogremovedupdate"></a>
- #### DialogRemovedUpdate
```csharp
public class DialogRemovedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, который удалил чат
    public required User User { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-useraddedupdate"></a>
- #### UserAddedUpdate
```csharp
public class UserAddedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, добавленный в чат
    public required User User { get; set; }

    // Пользователь, который добавил пользователя в чат. Может быть null, если пользователь присоединился к чату по ссылке
    public long? InviterId { get; set; }

    // Указывает, был ли пользователь добавлен в канал или нет
    public bool IsChannel { get; set; }
}
```
<a id="model-userremovedupdate"></a>
- #### UserRemovedUpdate
```csharp
public class UserRemovedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, удалённый из чата
    public required User User { get; set; }

    // Администратор, который удалил пользователя из чата. Может быть null, если пользователь покинул чат сам
    public long? AdminId { get; set; }

    // Указывает, был ли пользователь удалён из канала или нет
    public bool IsChannel { get; set; }
}
```
<a id="model-botstartedupdate"></a>
- #### BotStartedUpdate
```csharp
public class BotStartedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, добавивший бота в чат
    public required User User { get; set; }

    // Дополнительные данные из дип-линков, переданные при запуске бота
    public string? Payload { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-botstoppedupdate"></a>
- #### BotStoppedUpdate
```csharp
public class BotStoppedUpdate : Update
{
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, добавивший бота в чат
    public required User User { get; set; }

    // Текущий язык пользователя в формате IETF BCP 47
    public string? UserLocale { get; set; }
}
```
<a id="model-chattitlechangedupdate"></a>
- #### ChatTitleChangedUpdate
```csharp
public class ChatTitleChangedUpdate : Update
{
    // Новое название
    public string? Title { get; set; }
    // ID чата, где произошло событие
    public long ChatId { get; set; }

    // Пользователь, который изменил название
    public required User User { get; set; }
}
```
<a id="model-updatesresponse"></a>
#### UpdatesResponse
```csharp
public class UpdatesResponse
{
    // Страница обновлений
    public Update[]? Updates { get; set; }
    // Указатель на следующую страницу данных, если есть
    public long? Marker { get; set; }
}
```

<a id="model-uploaddataresponse"></a>
#### UploadDataResponse
```csharp
public class UploadDataResponse
{
    // Токен, используемый для прикрепления в AttachmentRequest, не используется при UploadType=Image
    public string? Token { get; set; }

    // Токены, полученные после загрузки изображений, только для UploadType=Image
    public Dictionary<string, UploadedInfo>? Photos { get; set; }
}
```

<a id="model-uploadedinfo"></a>
#### UploadedInfo
```csharp
public class UploadedInfo
{
    // Токен — уникальный ID загруженного медиафайла
    public required string Token { get; set; }
}
```

<a id="model-uploadurlresponse"></a>
#### UploadUrlResponse
```csharp
public class UploadUrlResponse
{
    public required string Url { get; set; }
    
    public string? Token { get; set; } 
}
```
<a id="model-user"></a>
#### User
```csharp
public class User : IEquatable<User>
{
    // int64 - ID пользователя
    public long UserID { get; set; }

    // string - Отображаемое имя пользователя
    public required string FirstName { get; set; } 

    // string [nullable] - Отображаемая фамилия пользователя
    public string? LastName { get; set; }

    // string [nullable] optional - deprecated
    public string? Name { get; set; }

    // string - Уникальное публичное имя пользователя. Может быть null, если пользователь недоступен или имя не задано 
    public string? UserName { get; set; }

    // bool - true, если пользователь является ботом
    public bool IsBot { get; set; }

    // int64 - Время последней активности пользователя в MAX (Unix-время в миллисекундах). Может быть неактуальным, если пользователь отключил статус "онлайн" в настройках.
    public DateTime LastActivityTime { get; set; }
}
```
<a id="model-userwithphoto"></a>
#### UserWithPhoto
```csharp
public class UserWithPhoto : User
{
    // до 16000 символов
    // Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
    public string? Description { get; set; }

    // URL аватара пользователя или бота в уменьшенном размере
    public string? AvatarUrl { get; set; }

    // URL аватара пользователя или бота в полном размере
    public string? FullAvatarUrl { get; set; }
}
```
<a id="model-videoinfo"></a>
#### VideoInfo
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

<a id="model-videourls"></a>
#### VideoUrls
```csharp
public class VideoUrls
{
    // URL видео в разрешении 1080p, если доступно
    public string? mp4_1080 { get; set; }
    
    // URL видео в разрешении 720p, если доступно
    public string? mp4_720 { get; set; }
    
    // URL видео в разрешении 480p, если доступно 
    public string? mp4_480 { get; set; }

    // URL видео в разрешении 360p, если доступно 
    public string? mp4_360 { get; set; }

    // URL видео в разрешении 240p, если доступно 
    public string? mp4_240 { get; set; }

    // URL видео в разрешении 144p, если доступно 
    public string? mp4_144 { get; set; }

    // URL трансляции, если доступна
    public string? HLS { get; set; } 
}
```

<a id="model-contactattachmentpayload"></a>
<a id="model-contactattachmentrequestpayload"></a>
<a id="model-fileattachmentpayload"></a>
<a id="model-inlinekeyboardattachmentrequestpayload"></a>
<a id="model-mediaattachmentpayload"></a>
<a id="model-photoattachmentpayload"></a>
<a id="model-photoattachmentrequestpayload"></a>
<a id="model-shareattachmentpayload"></a>
<a id="model-stickerattachmentpayload"></a>
<a id="model-stickerattachmentrequestpayload"></a>
<a id="model-videothumbnail"></a>
#### VideoThumbnail
```csharp
public class VideoThumbnail
{
    public required string Url { get; set; }
}
```

#### Внимание!!! На текущий момент в API нет метода для разблокировки заблокированных пользователей, пока это могут делать только администраторы вручную (Ticket: 4009805)

---
<a id="enums"></a>
## Перечисления типов 


<a id="enum-attachmenttype"></a>
#### AttachmentType
```csharp
public enum AttachmentType
{
    Image,
    Video,
    Audio,
    File,
    Sticker,
    Contact,
    InlineKeyboard,
    Share,
    Location,
}
```
<a id="enum-buttontype"></a>
#### ButtonType
```csharp
public enum ButtonType
{
    // Кнопка обработки события
    Callback,
    // Кнопка-ссылка
    Link,
    // Кнопка запроса геолокации
    RequestGeoLocation,
    // Кнопка запроса контакта
    RequestContact,
    // Кнопка открытия приложения
    OpenApp,
    // Кнопка сообщения (текст кнопки будет использован как выбор ответа в сообщении при нажатии)
    Message
}
```
<a id="enum-chatadminpermissions"></a>
#### ChatAdminPermissions
```csharp
public enum ChatAdminPermission
{
    ReadAllMessages,
    AddRemoveMembers,
    AddAdmins,
    ChangeChatInfo,
    PinMessage,
    Write,
    CanCall,
    EditLink,
    PostEditDeleteMessage,
    EditMessage,
    DeleteMessage,
    ViewStats,
    Delete,
    Edit   
}
```
<a id="enum-chatstatus"></a>
#### ChatStatus
```csharp
public enum ChatStatus
{
    Active,
    Removed,
    Left,
    Closed
}
```
<a id="enum-chattype"></a>
#### ChatType
```csharp
public enum ChatType
{
    // групповой чат
    Chat,
    // личные сообщения
    Dialog,
    // канал
    Channel
```
<a id="enum-markupelementtype"></a>
#### MarkupElementType
```csharp
public enum MarkupElementType
{
    // жирный
    Strong ,
    // курсив
    Emphasized,
    // моноширинный
    Monospaced,
    // ссылка
    Link,
    // зачеркнутый
    Strikethrough,
    // подчеркнутый
    Underline, 
    // упоминание пользователя
    UserMention
}
```
<a id="enum-messagelinktype"></a>
#### MessageLinkType
```csharp
public enum MessageLinkType
{
    // Пересланное сообщение
    Forward,
    // Ответ на сообщение
    Reply
}
```
<a id="enum-senderaction"></a>
#### SenderAction
```csharp
public enum SenderAction
{
    // печатает...
    TypingOn,
    // отправляет фото..
    SendingPhoto,
    // отправляет видео...
    SendingVideo,
    // отправляет аудио/голосовое
    SendingAudio,
    // отправляет файл
    SendingFile,
    // отмечает сообщения прочитанными
    MarkSeen
}
```
<a id="enum-textformat"></a>
#### TextFormat
```csharp
public enum TextFormat
{
    Markdown,
    HTML
}
```

<a id="enum-updatetype"></a>
#### UpdateType
```csharp
public enum UpdateType
{
    // Новое сообщение
    MessageCreated,
    // Событие callback (нажата кнопка)
    MessageCallback,
    // Сообщение отредактировано
    MessageEdited,
    // Сообщение удалено
    MessageRemoved,
    // Бот добавлен в группу
    BotAdded,
    // Бот исключен из группы
    BotRemoved,
    // Уведомления в диалоге отключены
    DialogMuted,
    // Уведомления в диалоге включены
    DialogUnmuted,
    // Диалог очищен (все сообщения удалены)
    DialogCleared,
    // Диалог удален
    DialogRemoved,
    // Пользователь добавлен в группу 
    UserAdded,
    // Пользователь удален из группы
    UserRemoved,
    // Начат диалог с ботом
    BotStarted,
    // Прекращен диалог с ботом
    BotStopped,
    // Произошла смена названия группы
    ChatTitleChanged
}
```
<a id="enum-uploadtype"></a>
#### UploadType
```csharp
public enum UploadType
{
    // image: JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
    Image,
    // video: MP4, MOV, MKV, WEBM, MATROSKA
    Video,
    // audio: MP3, WAV, M4A и другие
    Audio,
    // file: любые типы файлов
    File
}
```

---
<a id="example"></a>
## Пример для minimal api (webhook)

```csharp
using MaxBot;
using MaxBotApi;
using MaxBotApi.Enums;
using MaxBotApi.Models;
string token = "токен бота";
string webhookUrl = "https://адресвашегобота/bot";
string secret = "проверочныйкод"; // этим кодом будет сопровождаться каждый запрос от платформы
var bot = new MaxBotClient(token);
await bot.SetWebhook(webhookUrl, secret);

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
            await bot.SendMessageToChat(message.Recipient.ChatId, message.MessageBody?.Text);
        }        
    }    
}
```

<a id="example2"></a>
#### Минимальный рабочий пример long-polling исполнения:
```csharp
using MaxBotApi;
using MaxBotApi.Enums;
using MaxBotApi.Models;

CancellationTokenSource cts = new();
string botToken = "токен вашего бота";
MaxBotClient  bot = new(botToken, cancellationToken: cts.Token);

EventWaitHandle waitHandle = new(false, EventResetMode.AutoReset, null, out bool createdNew);
bool signaled;

bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;

var me = await bot.GetMe();
Console.WriteLine(string.Format("@{0} запущен... Наберите 'shutdown' в лс боту для завершения", me.FirstName));
if (!createdNew)
    waitHandle.Set();
else
    do { signaled = waitHandle.WaitOne(TimeSpan.FromMilliseconds(33)); }
    while (!signaled);

cts.Cancel(); // stop the bot
Console.WriteLine("exit!");

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception.ToString());
    await Task.CompletedTask;
}

async Task OnUpdate(Update update)
{
    Console.WriteLine(update);
    await Task.CompletedTask;
}

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.MessageBody?.Text == "shutdown")
    {
        waitHandle.Set();
    }
    Console.WriteLine(msg);
    await Task.CompletedTask;
}
```


---
#### Данный клиент будет постепенно дорабатываться, документация дополняться.

#### По любым вопросам, связанным с данным кодом и документацией к нему, можно писать мне в MAX
### [🔗 MaxBotApi - Группа разработки](https://max.ru/join/rGHhNOyyFyG4p2I7IwryhaWPxecPHqykNC0plzA3X2Q)
