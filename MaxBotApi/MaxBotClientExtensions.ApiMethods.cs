using MaxBotApi.Enums;
using MaxBotApi.Extensions;
using MaxBotApi.Models;
using MaxBotApi.Models.Payloads;
using MaxBotApi.Requests;
using MaxBotApi.Requests.Upload;
using MaxBotApi.Types;

namespace MaxBotApi;

public static partial class MaxBotClientExtensions
{
    extension(IMaxBotClient botClient)
    {
        #region Subscriptions

        /// <summary>
        /// Подписывает бота на получение обновлений через WebHook. После вызова этого метода бот будет получать уведомления о новых событиях в чатах на указанный URL. Ваш сервер должен прослушивать один из следующих портов: 80, 8080, 443, 8443, 16384-32383
        /// </summary>
        /// <param name="url">URL HTTP(S)-эндпойнта вашего бота. Должен начинаться с http(s)://</param>
        /// <param name="updateTypes">Список типов обновлений, которые ваш бот хочет получать. Для полного списка типов см. объект Update</param>
        /// <param name="secretToken">от 5 до 256 символов. Cекрет, который должен быть отправлен в заголовке X-Max-Bot-Api-Secret в каждом запросе Webhook. Разрешены только символы A-Z, a-z, 0-9, и дефис. Заголовок рекомендован, чтобы запрос поступал из установленного веб-узла</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> SetWebhook(string url, string? secretToken = null, IEnumerable<UpdateType>? updateTypes = null,
            CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new SetWebhookRequest
            {
                Url = url,
                UpdateTypes = updateTypes ?? Update.AllTypes,
                Secret = secretToken,
            }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Отписывает бота от получения обновлений через Webhook. После вызова этого метода бот перестаёт получать уведомления о новых событиях, и становится доступна доставка уведомлений через API с длительным опросом
        /// </summary>
        /// <param name="url">URL, который нужно удалить из подписок на WebHook</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> DeleteWebhook(string url, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new DeleteWebhookRequest(url), cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Если ваш бот получает данные через Webhook, этот метод возвращает список всех подписок. При настройке уведомлений для production-окружения рекомендуем использовать Webhook
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Subscriptions</returns>
        public async Task<Subscriptions> GetWebhookInfo(CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetWebhookInfoRequest(), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Этот метод можно использовать для получения обновлений при разработке и тестировании, если ваш бот не подписан на Webhook. Для production-окружения рекомендуем использовать Webhook
        /// Метод использует долгий опрос (long polling). Каждое обновление имеет свой номер последовательности. Свойство marker в ответе указывает на следующее ожидаемое обновление.
        /// Все предыдущие обновления считаются завершёнными после прохождения параметра marker. Если параметр marker не передан, бот получит все обновления, произошедшие после последнего подтверждения
        /// </summary>
        /// <param name="limit">По умолчанию: 100. Максимальное количество обновлений для получения</param>
        /// <param name="timeout">По умолчанию: 30. Тайм-аут в секундах для долгого опроса</param>
        /// <param name="marker">Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления</param>
        /// <param name="types">Список типов обновлений, которые бот хочет получить (например, message_created, message_callback)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>UpdatesResponse</returns>
        public async Task<UpdatesResponse> GetUpdates(int limit = 100, int timeout = 30, long? marker = null, IEnumerable<UpdateType>? types = null,
            CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetUpdatesRequest()
            {
                Limit = limit, Timeout = timeout, Types = types, Marker = marker
            }, cancellationToken).ConfigureAwait(false);

        #endregion

        #region Bot

        /// <summary>
        /// Метод возвращает информацию о боте, который идентифицируется с помощью токена доступа access_token. В ответе приходит объект User с вариантом наследования BotInfo, который содержит идентификатор бота, его название, никнейм, время последней активности, описание и аватар (при наличии)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>BotInfo</returns>
        public async Task<BotInfo> GetMe(CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetMeRequest(), cancellationToken).ConfigureAwait(false);

        #endregion

        #region Upload

        /// <summary>
        /// Делает полную операцию по загрузке файла: получет ссылку на загрузку и по ней загружает файл
        /// </summary>
        /// <param name="type">Тип загружаемого файла
        /// image: JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
        /// video: MP4, MOV, MKV, WEBM, MATROSKA
        /// audio: MP3, WAV, M4A и другие
        /// file: любые типы файлов
        /// </param>
        /// <param name="filename">Путь к файлу на диске</param>
        /// <param name="cancellationToken"></param>
        /// <returns>UploadDataResponse</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task<UploadDataResponse> UploadFile(UploadType type, string filename, CancellationToken cancellationToken = default)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename);
            var response = await botClient.ThrowIfNull().SendRequest(new UploadRequest(type), cancellationToken).ConfigureAwait(false);
            var result=  await botClient.ThrowIfNull().SendFile(new UploadDataRequest(response.Url) { FileName = filename, Token = response.Url }, cancellationToken)
                .ConfigureAwait(false);
            if (result.Retval.HasValue && result.Token is null)
                result.Token = response.Token;
            return result;
        }

        #endregion

        #region Messages

        /// <summary>
        /// Метод возвращает информацию о сообщении или массив сообщений из чата.
        /// Сообщения возвращаются в обратном порядке: последние сообщения будут первыми в массиве
        /// </summary>
        /// <param name="chat_id">ID чата, чтобы получить сообщения из определённого чата
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>MessagesResponse</returns>
        public async Task<MessagesResponse> GetMessages(long chat_id, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetMessagesRequest(chat_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Метод возвращает информацию о сообщении или массив сообщений из чата.
        /// Можно указать один идентификатор или несколько
        /// </summary>
        /// <param name="messages_ids">Список ID сообщений, которые нужно получить (через запятую)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>MessagesResponse</returns>
        public async Task<MessagesResponse> GetMessages(IEnumerable<string> messages_ids, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetMessagesRequest(messages_ids), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Метод возвращает информацию о сообщении или массив сообщений из чата.
        /// </summary>
        /// <param name="chat_id">ID чата, чтобы получить сообщения из определённого чата</param>
        /// <param name="messages_ids">Список ID сообщений, которые нужно получить (через запятую)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>MessagesResponse</returns>
        public async Task<MessagesResponse> GetMessages(long chat_id, IEnumerable<string> messages_ids, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetMessagesRequest(chat_id, messages_ids), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Отправляет сообщение в чат пользователю
        /// </summary>
        /// <param name="user_id">ID пользователя</param>
        /// <param name="text">до 4000 символов. Новый текст сообщения</param>
        /// <param name="disable_link_preview">Если false, сервер не будет генерировать превью для ссылок в тексте сообщения</param>
        /// <param name="notify">Если false, участники чата не будут уведомлены (по умолчанию true)</param>
        /// <param name="text_format">Markdown или HTML. Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0</param>
        /// <param name="link">Ссылка на сообщение (для репостов)</param>
        /// <param name="attachments">Вложения сообщения. Если пусто, все вложения будут удалены</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiMessage</returns>
        public async Task<ApiMessage> SendMessage(long user_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumerable<AttachmentRequest>? attachments = null,
            CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new SendMessageToUserRequest(user_id, disable_link_preview)
            {
                Text = text,
                Attachments = attachments,
                Notify = notify,
                Format = text_format ?? TextFormat.HTML,
                Link = link,
            }, cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Отправляет сообщение в чат группы\канала
        /// </summary>
        /// <param name="chat_id">ID группы\канала</param>
        /// <param name="text">до 4000 символов. Новый текст сообщения</param>
        /// <param name="disable_link_preview">Если false, сервер не будет генерировать превью для ссылок в тексте сообщения</param>
        /// <param name="notify">Если false, участники чата не будут уведомлены (по умолчанию true)</param>
        /// <param name="text_format">Markdown или HTML. Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0</param>
        /// <param name="link">Ссылка на сообщение (для репостов)</param>
        /// <param name="attachments">Вложения сообщения. Если пусто, все вложения будут удалены</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiMessage</returns>
        public async Task<ApiMessage> SendMessageToChat(long chat_id, string? text, bool disable_link_preview = false, bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumerable<AttachmentRequest>? attachments = null,
            CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new SendMessageToChatRequest(chat_id, disable_link_preview)
            {
                Text = text,
                Attachments = attachments,
                Notify = notify,
                Format = text_format ?? TextFormat.HTML,
                Link = link,
            }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Редактирует сообщение в чате. Если поле attachments равно null, вложения текущего сообщения не изменяются. Если в этом поле передан пустой список, все вложения будут удалены
        /// </summary>
        /// <param name="message_id">ID редактируемого сообщения</param>
        /// <param name="text">до 4000 символов. Новый текст сообщения</param>
        /// <param name="attachments">Вложения сообщения</param>
        /// <param name="link">Ссылка на сообщение (для репостов)</param>
        /// <param name="notify">Если false, участники чата не будут уведомлены (по умолчанию true)</param>
        /// <param name="text_format">Markdown или HTML. Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> EditMessage(string message_id, string? text = null, IEnumerable<AttachmentRequest>? attachments = null,
            NewMessageLink? link = null,
            bool notify = true, TextFormat text_format = TextFormat.HTML, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new EditMessageRequest(message_id)
            {
                Text = text,
                Attachments = attachments,
                Link = link,
                Notify = notify,
                TextFormat = text_format,
            }, cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Удаляет сообщение в диалоге или чате, если бот имеет разрешение на удаление сообщений
        /// С помощью метода можно удалять сообщения, которые отправлены менее 24 часов назад
        /// </summary>
        /// <param name="messageId">ID удаляемого сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> DeleteMessage(string messageId, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new DeleteMessageRequest(messageId), cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Этот метод используется для отправки ответа после того, как пользователь нажал на кнопку. Ответом может быть обновленное сообщение и/или одноразовое уведомление для пользователя
        /// </summary>
        /// <param name="callback_id">Идентификатор кнопки, по которой пользователь кликнул. Бот получает идентификатор как часть Update с типом message_callback.</param>
        /// <param name="newMessageBody">Заполните это, если хотите изменить текущее сообщение</param>
        /// <param name="notification">Заполните это, если хотите просто отправить одноразовое уведомление пользователю</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> SendCallbackReact(string callback_id, NewMessageBody? newMessageBody = null, string? notification = null,
            CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new SendCallbackReactRequest(callback_id)
                {
                    Message = newMessageBody,
                    Notification = notification,
                },
                cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Возвращает сообщение по его ID
        /// </summary>
        /// <param name="message_id">ID сообщения (mid), чтобы получить одно сообщение в чате</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessage(string message_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetMessageRequest(message_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Возвращает подробную информацию о прикреплённом видео. URL-адреса воспроизведения и дополнительные метаданные
        /// </summary>
        /// <param name="video_token">Токен видео-вложения</param>
        /// <param name="cancellationToken"></param>
        /// <returns>VideoInfo</returns>
        public async Task<VideoInfo> GetVideoInfo(string video_token, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetVideoInfoRequest(video_token), cancellationToken).ConfigureAwait(false);

        #endregion

        #region Chat

        /// <summary>
        /// Возвращает список групповых чатов, в которых участвовал бот, информацию о каждом чате и маркер для перехода к следующей странице списка
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatsResponse</returns>
        public async Task<ChatsResponse> GetChats(CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetChatsRequest(), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Возвращает список групповых чатов, в которых участвовал бот, информацию о каждом чате и маркер для перехода к следующей странице списка
        /// </summary>
        /// <param name="count">Количество запрашиваемых чатов, по умолчанию 50</param>
        /// <param name="marker">Указатель на следующую страницу данных. Для первой страницы передайте null</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatsResponse</returns>
        public async Task<ChatsResponse> GetChats(int count = 50, long? marker = null, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatsRequest(count, marker), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает информацию о групповом чате по его ID
        /// </summary>
        /// <param name="chat_id">ID запрашиваемого чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatFullInfo</returns>
        public async Task<Chat> GetChat(long chat_id, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetChatInfoRequest(chat_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Позволяет редактировать информацию о групповом чате, включая название, иконку и закреплённое сообщение
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="icon">Запрос на прикрепление изображения (все поля являются взаимоисключающими)</param>
        /// <param name="title">от 1 до 200 символов</param>
        /// <param name="pin">ID сообщения для закрепления в чате. Чтобы удалить закреплённое сообщение, используйте метод unpin</param>
        /// <param name="notify">Если true, участники получат системное уведомление об изменении</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatFullInfo</returns>
        public async Task<Chat> EditChatInfo(long chat_id, PhotoAttachmentRequestPayload? icon = null, string? title = null, string? pin = null,
            bool? notify = null, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new EditChatInfoRequest(chat_id)
            {
                Icon = icon,
                Title = title,
                Pin = pin,
                Notify = notify,
            }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Удаляет групповой чат для всех участников
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> DeleteChat(long chat_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new DeleteChatRequest(chat_id), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Позволяет отправлять в групповой чат такие действия бота, как например: «набор текста» или «отправка фото»
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="action">Действие, отправляемое участникам чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> SendChatAction(long chat_id, SenderAction action, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new SendChatActionRequest(chat_id) { Action = action }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Возвращает закреплённое сообщение в групповом чате
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiMessage</returns>
        public async Task<ApiMessage> GetChatPinnedMessage(long chat_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatPinnedMessageRequest(chat_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Закрепляет сообщение в групповом чате
        /// </summary>
        /// <param name="chat_id">ID чата, где должно быть закреплено сообщение</param>
        /// <param name="message_id">ID сообщения, которое нужно закрепить. Соответствует полю Message.body.mid</param>
        /// <param name="notify">Если true, участники получат уведомление с системным сообщением о закреплении</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> PinMessage(long chat_id, string message_id, bool notify = true, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new PinMessageRequest(chat_id)
            {
                MessageId = message_id,
                Notify = notify,
            }, cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Удаляет закреплённое сообщение в групповом чате
        /// </summary>
        /// <param name="chat_id">ID чата, из которого нужно удалить закреплённое сообщение</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> UnpinMessage(long chat_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new UnpinMessageRequest(chat_id), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает информацию о членстве текущего бота в групповом чате. Бот идентифицируется с помощью токена доступа
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatMember</returns>
        public async Task<ChatMember> GetChatMyInfo(long chat_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatMyInfoRequest(chat_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Удаляет бота из участников группового чата
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> LeaveChat(long chat_id, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new LeaveChatRequest(chat_id), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Возвращает список всех администраторов группового чата. Бот должен быть администратором в запрашиваемом чате
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="marker">Указатель на следующую страницу данных</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatMembersResponse</returns>
        public async Task<ChatMembersResponse> GetChatAdmins(long chat_id, long? marker = null, CancellationToken cancellationToken = default) =>
            await botClient.ThrowIfNull().SendRequest(new GetChatAdminsRequest(chat_id, marker), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает значение true, если в групповой чат добавлены все администраторы
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="admins">Список пользователей, которые получат права администратора чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> AddChatAdmins(long chat_id, IEnumerable<ChatAdmin> admins, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new AddChatAdminsRequest(chat_id) { Admins = admins }, cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Отменяет права администратора у пользователя в групповом чате, лишая его административных привилегий
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="user_id">ID пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> DeleteChatAdmin(long chat_id, long user_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new DeleteChatAdminRequest(chat_id, user_id), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает список участников группового чата
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatMembersResponse</returns>
        public async Task<ChatMembersResponse> GetChatMembers(long chat_id, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatMembersRequest(chat_id), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает список участников группового чата
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="user_ids">Список ID пользователей, чье членство нужно получить. Когда этот параметр передан, параметры count и marker игнорируются</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatMembersResponse</returns>
        public async Task<ChatMembersResponse> GetChatMembers(long chat_id, IEnumerable<long> user_ids, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatMembersRequest(chat_id, user_ids), cancellationToken).ConfigureAwait(false);


        /// <summary>
        /// Возвращает список участников группового чата
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="marker">Указатель на следующую страницу данных</param>
        /// <param name="count">Количество участников, которых нужно вернуть [1-100], по умолчанию 20</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ChatMembersResponse</returns>
        public async Task<ChatMembersResponse> GetChatMembers(long chat_id, long marker, int count = 20, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new GetChatMembersRequest(chat_id, marker, count), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Добавляет участников в групповой чат. Для этого могут потребоваться дополнительные права
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="user_ids">Массив ID пользователей для добавления в чат</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> InviteUser(long chat_id, IEnumerable<long> user_ids, CancellationToken cancellationToken = default)
            => await botClient.ThrowIfNull().SendRequest(new InviteUserRequest(chat_id) { UserIds = user_ids }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Удаляет участника из группового чата. Для этого могут потребоваться дополнительные права
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="user_id">ID пользователя, которого нужно удалить из чата</param>
        /// <param name="block">Если установлено в true, пользователь будет заблокирован в чате. Применяется только для чатов с публичной или приватной ссылкой. Игнорируется в остальных случаях</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse> KickUser(long chat_id, long user_id, bool block = false, CancellationToken cancellationToken = default)
            => block
                ? await botClient.ThrowIfNull().SendRequest(new KickUserRequest(chat_id, user_id), cancellationToken).ConfigureAwait(false)
                : await botClient.ThrowIfNull().SendRequest(new KickBanRequest(chat_id, user_id), cancellationToken).ConfigureAwait(false);

        #endregion
    }
}