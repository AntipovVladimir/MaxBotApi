using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi;

public static partial class MaxBotClientExtensions
{
   
    extension(IMaxBotClient client)
    {
        /// <summary>
        /// Вспомогательный метод для ответа на входящее сообщение
        /// </summary>
        /// <param name="message">Входящее сообщение</param>
        /// <param name="text">до 4000 символов. Новый текст сообщения</param>
        /// <param name="disable_link_preview">Если false, сервер не будет генерировать превью для ссылок в тексте сообщения</param>
        /// <param name="notify">Если false, участники чата не будут уведомлены (по умолчанию true)</param>
        /// <param name="text_format">Markdown или HTML. Если установлен, текст сообщения будет форматирован данным способом. Для подробной информации загляните в раздел Форматирование https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0</param>
        /// <param name="link">Ссылка на сообщение (для репостов)</param>
        /// <param name="attachments">Вложения сообщения. Если пусто, все вложения будут удалены</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ApiMessage</returns>
        public async Task<ApiMessage> ReplyMessage(Message message, string? text, bool disable_link_preview = false,
            bool notify = true,
            TextFormat? text_format = null,
            NewMessageLink? link = null, IEnumerable<AttachmentRequest>? attachments = null,
            CancellationToken cancellationToken = default) =>
            message.Recipient.ChatType == ChatType.Dialog
                ? await client.SendMessage(message.Sender.UserID, text, disable_link_preview, notify, text_format, link, attachments, cancellationToken)
                : await client.SendMessageToChat(message.Recipient.ChatId, text, disable_link_preview, notify, text_format, link, attachments, cancellationToken);
    }
}