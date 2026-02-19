namespace MaxBotApi.Enums;

/// <summary>The source of the error</summary>
public enum HandleErrorSource
{
    /// <summary>Exception occured during GetUpdates. Polling of updates will continue</summary>
    PollingError,
    /// <summary>A fatal uncaught exception occured somewhere. Polling of updates will stop</summary>
    FatalError,
    /// <summary>Exception was thrown by HandleUpdateAsync. Polling of updates will continue</summary>
    HandleUpdateError,
}  