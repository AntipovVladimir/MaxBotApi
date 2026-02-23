using MaxBotApi.Enums;
using MaxBotApi.Models;
using MaxBotApi.Polling;

namespace MaxBotApi;

public static partial class MaxBotClientExtensions
{
    /// <param name="botClient">The <see cref="IMaxBotClient"/> used for making GetUpdates calls</param>
    extension(IMaxBotClient botClient)
    {
        public void StartReceiving<TUpdateHandler>(ReceiverOptions? receiverOptions = null,
            CancellationToken cancellationToken = default) where TUpdateHandler : IUpdateHandler, new()
            => StartReceiving(botClient, new TUpdateHandler(), receiverOptions, cancellationToken);

        public void StartReceiving(Func<IMaxBotClient, Update, CancellationToken, Task> updateHandler,
            Func<IMaxBotClient, Exception, HandleErrorSource, CancellationToken, Task> errorHandler,
            ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
            => StartReceiving(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken);

        public void StartReceiving(Func<IMaxBotClient, Update, CancellationToken, Task> updateHandler,
            Func<IMaxBotClient, Exception, CancellationToken, Task> errorHandler,
            ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
            => StartReceiving(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken);

        public void StartReceiving(Action<IMaxBotClient, Update, CancellationToken> updateHandler,
            Action<IMaxBotClient, Exception, CancellationToken> errorHandler,
            ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
            => StartReceiving(botClient, new DefaultUpdateHandler(
                (bot, update, token) =>
                {
                    updateHandler(bot, update, token);
                    return Task.CompletedTask;
                },
                (bot, exception, source, token) =>
                {
                    errorHandler(bot, exception, token);
                    return Task.CompletedTask;
                }
            ), receiverOptions, cancellationToken);

        /// <summary>Starts receiving <see cref="Update"/>s on the ThreadPool, invoking <see cref="IUpdateHandler.HandleUpdateAsync">IUpdateHandler.HandleUpdateAsync</see> for each.
        /// <para>This method does not block. A background polling loop is initiated, calling GetUpdates then your handler for each update</para></summary>
        /// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
        /// <param name="receiverOptions">Options used to configure getUpdates request</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public void StartReceiving(IUpdateHandler updateHandler, ReceiverOptions? receiverOptions = null,
            CancellationToken cancellationToken = default)
        {
            if (botClient is null)
            {
                throw new ArgumentNullException(nameof(botClient));
            }

            if (updateHandler is null)
            {
                throw new ArgumentNullException(nameof(updateHandler));
            }

            // ReSharper disable once MethodSupportsCancellation
            _ = Task.Run(async () =>
            {
                try
                {
                    await ReceiveAsync(botClient, updateHandler, receiverOptions, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
                catch (Exception ex)
                {
                    try
                    {
                        await updateHandler.HandleErrorAsync(botClient, ex, HandleErrorSource.FatalError, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // ignored
                    }
                }
            }, cancellationToken);
        }

        public async Task ReceiveAsync<TUpdateHandler>(ReceiverOptions? receiverOptions = null,
            CancellationToken cancellationToken = default) where TUpdateHandler : IUpdateHandler, new()
            => await ReceiveAsync(botClient, new TUpdateHandler(), receiverOptions, cancellationToken).ConfigureAwait(false);

        public async Task ReceiveAsync(Func<IMaxBotClient, Update, CancellationToken, Task> updateHandler,
            Func<IMaxBotClient, Exception, CancellationToken, Task> errorHandler,
            ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
            => await ReceiveAsync(botClient, new DefaultUpdateHandler(updateHandler, errorHandler), receiverOptions, cancellationToken).ConfigureAwait(false);

        public async Task ReceiveAsync(Action<IMaxBotClient, Update, CancellationToken> updateHandler,
            Action<IMaxBotClient, Exception, CancellationToken> errorHandler,
            ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
            => await ReceiveAsync(botClient, new DefaultUpdateHandler(
                (bot, update, token) =>
                {
                    updateHandler(bot, update, token);
                    return Task.CompletedTask;
                },
                (bot, exception, source, token) =>
                {
                    errorHandler(bot, exception, token);
                    return Task.CompletedTask;
                }
            ), receiverOptions, cancellationToken).ConfigureAwait(false);

        public async Task ReceiveAsync(IUpdateHandler updateHandler, ReceiverOptions? receiverOptions = null, CancellationToken cancellationToken = default)
        {
            if (updateHandler is null)
            {
                throw new ArgumentNullException(nameof(updateHandler));
            }

            var allowedUpdates = receiverOptions?.AllowedUpdates;
            var limit = receiverOptions?.Limit ?? 100;
            var messageOffset = receiverOptions?.Offset;

            if (receiverOptions?.DropPendingUpdates is true)
            {
                try
                {
                    var updates = await botClient.GetUpdates(null,null,null, allowedUpdates, cancellationToken).ConfigureAwait(false);
                    if (updates.Marker != 0)
                        messageOffset = updates.Marker;
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }

            var request = new Requests.GetUpdatesRequest
            {
                Limit = limit,
                Marker = messageOffset,
                Types = allowedUpdates,
            };
            while (!cancellationToken.IsCancellationRequested)
            {
                request.Timeout = (int)botClient.Timeout.TotalSeconds;
                UpdatesResponse? updates = null;
                try
                {
                    updates = await botClient.SendRequest(request, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception exception)
                {
                    try
                    {
                        await updateHandler.HandleErrorAsync(botClient, exception, HandleErrorSource.PollingError, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                }

                if (updates?.Updates is null) continue;
                request.Marker = updates.Marker;
                foreach (var update in updates.Updates)
                {
                    try
                    {
                        await updateHandler.HandleUpdateAsync(botClient, update, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            await updateHandler.HandleErrorAsync(botClient, ex, HandleErrorSource.HandleUpdateError, cancellationToken).ConfigureAwait(false);
                        }
                        catch (OperationCanceledException)
                        {
                            // ignored
                        }
                    }
                }
            }
        }
    }
}