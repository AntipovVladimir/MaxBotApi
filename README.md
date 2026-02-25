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

### изменения 1.0.7
+ добавлена поддержка **long-polling** (из документации: Long Polling — для разработки и тестирования, только Webhook — для production-окружения)
+ добавлен метод AnswerCallback идентичный SendCallbackReact, более привычный для тех кто переносит код с телеграм-бота

### Данный клиент будет постепенно дорабатываться, документация дополняться.

###### По вопросам связанным с данным кодом можно писать мне в MAX https://max.ru/join/rGHhNOyyFyG4p2I7IwryhaWPxecPHqykNC0plzA3X2Q

###### либо в телеграм https://t.me/darkagent