using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Polling;

public class DefaultUpdateHandler(
    Func<IMaxBotClient, Update, CancellationToken, Task> updateHandler,
    Func<IMaxBotClient, Exception, HandleErrorSource, CancellationToken, Task> errorHandler) : IUpdateHandler
{
    /// <summary>Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions</summary>
    /// <param name="updateHandler">The function to invoke when an update is received</param>
    /// <param name="errorHandler">The function to invoke when an error occurs</param>
    public DefaultUpdateHandler(
        Func<IMaxBotClient, Update, CancellationToken, Task> updateHandler,
        Func<IMaxBotClient, Exception, CancellationToken, Task> errorHandler)
        : this(updateHandler, (bot, ex, s, ct) => errorHandler(bot, ex, ct))
    { }

    /// <inheritdoc/>
    public async Task HandleUpdateAsync(IMaxBotClient botClient, Update update, CancellationToken cancellationToken)
        => await updateHandler(botClient, update, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task HandleErrorAsync(IMaxBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        => await errorHandler(botClient, exception, source, cancellationToken).ConfigureAwait(false);
} 