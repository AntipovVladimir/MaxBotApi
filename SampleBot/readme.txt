Данный бот вызывается заббиксом: 
Настройки в заббиксе: Alerts -> Media Types -> create media type
Name = Max Bot
Type = Webhook
Url = указываем ссылку на сервер, где размещен бот (в program.cs этот же сервер используется в webhookUrl, только вместо /bot пишем /zabbix, например https://zbx.test.ru/zabbix)
To, Subject и Message оставляем по умолчанию


Для сборки и работы потребуется .NET 10 (dotnet-sdk-10), инструкции по установке под вашу ОС смотрим здесь: https://learn.microsoft.com/en-us/dotnet/core/install/
Будет работать как под windows так и под linux. 

Как собрать бота:
1. прежде всего надо поправить в Program.cs
string token = "токен бота"; - указать токен бота, сгенерированный в бизнес-портале max
string serverAddress = "https://адресвашегосервера"; - указать адрес сервера где будет доступен бот 
string botPath = "/bot"; - путь для запросов от платформы, рекомендуется оставить по умолчанию
string secret = "проверочныйкод"; // этим кодом будет сопровождаться каждый запрос от платформы - рекомендуется указать какую-нибудь случайную текстовую абракадабру в 16-32 символа

2. создаем пользователя и домашнюю директорию для готового бота
adduser --system --shell=/bin/false --home=/home/bot bot

3. в директории с проектом запускае dotnet publish -c Debug -o /home/bot
(можно вместо Debug указать Release, если вместо System.Diagnostics.Debug.WriteLine используете отдельный логер)

4. запускаем /home/bot/SampleBot

Боту в личные сообщения пишем: 
/auth теперь я твой хозяин
и бот запоминает что вы его хозяин
затем добавляем бота в группу или канал куда хотим чтобы приходили сообщения от заббикса (добавлять должен хозяин бота)


Чтобы бот запускался как служба, необходимо подготовить дескриптор службы.

Инструкция для linux / systemd:
создаем файл сервиса для systemd: 
nano /lib/systemd/system/bot.service
со следующим содержимым:

[Unit]
Description=SampleBot

[Service]
WorkingDirectory=/home/bot
ExecStart=/home/bot/SampleBot
SyslogIdentifier=SampleBot
User=bot
Restart=always
RestartSec=5

KillSignal=SIGINT
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target

сохраняем
далее 
sudo systemctl daemon-reload

если нужен автозапуск бота:
sudo systemctl enable bot

запуск и остановка службы через команды
sudo systemctl start bot
sudo systemctl stop bot