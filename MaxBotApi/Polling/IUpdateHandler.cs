using MaxBotApi.Enums;
using MaxBotApi.Models;

namespace MaxBotApi.Polling;

public interface IUpdateHandler
{
    /// <summary>Handles an <see cref="Update"/></summary>
    /// <param name="botClient">The <see cref="IMaxBotClient"/> instance of the bot receiving the <see cref="Update"/></param>
    /// <param name="update">The <see cref="Update"/> to handle</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which will notify that method execution should be cancelled</param>
    Task HandleUpdateAsync(IMaxBotClient botClient, Update update, CancellationToken cancellationToken);

    /// <summary>Handles an <see cref="Exception"/></summary>
    /// <param name="botClient">The <see cref="IMaxBotClient"/> instance of the bot receiving the <see cref="Exception"/></param>
    /// <param name="exception">The <see cref="Exception"/> to handle</param>
    /// <param name="source">Where the error occured</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which will notify that method execution should be cancelled</param>
    Task HandleErrorAsync(IMaxBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken);
} 