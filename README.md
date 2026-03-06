## .NET Client for Max Bot API

Используется **.NET10 C#14**, построено для работы через webhook (поддерживается long-polling).

Для разработки использовалась официальная документация
https://dev.max.ru/docs-api

### Доступно в nuget:
https://www.nuget.org/packages/MaxBotApi
```
dotnet add package MaxBotApi
```
### Актуальная документация по MaxBotApi теперь здесь: 
https://github.com/AntipovVladimir/MaxBotApi/blob/main/MaxBotApi/Docs/README.md

### изменения 1.0.10
+ Добавлен механизм повторных запросов при отправке сообщения, вложения которого требуют время на обработку платформой. Обработка ошибки Key: errors.process.attachment.file.not.processed
При возникновении ошибки "errors.process.attachment.file.not.processed", будет произведена повторная попытка отправки сообщения. Задержка перед повторной попыткой расчитывается по формуле [номер попытки * 5 сек], за количество попыток в **MaxBotClientOptions** отвечает 
**RetryWaitAttachment** (значение по умолчанию - **10**, итого, максимум времени ожидания - до 275 сек), после этого исключение будет выброшено во внешнюю обработку. 
### изменения 1.0.9
+ [FIX] не обрабатывались апдейты типа used_added/user_removed

### изменения 1.0.8
+ добавлен метод расширения **ReplyMessage**

### Данный клиент будет постепенно дорабатываться, документация дополняться.

###### По вопросам связанным с данным кодом можно писать мне в MAX https://max.ru/join/rGHhNOyyFyG4p2I7IwryhaWPxecPHqykNC0plzA3X2Q

###### либо в телеграм https://t.me/darkagent