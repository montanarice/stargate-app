using StargateAPI.Business.Data;

namespace StargateAPI.Business.Logging;

public class DatabaseLogger : ILogger
{
    // TODO: Test logger class
    // TODO: Hard coding as stargate context for data access for now. Fix later.
    private readonly StargateContext _dataAccess;

    public DatabaseLogger(StargateContext dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        // TODO: Come back to this if time
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // TODO CRITICAL: Test this
        if (!IsEnabled(logLevel)) return;

        // TODO: Many obvious improvements to log output here
        string exceptionMessage = exception?.Message ?? "No exception message found";

        // TODO: Handle this cleanly async 
        _dataAccess.LogTables.Add(new LogTableEntry { Message = exceptionMessage });
        _dataAccess.SaveChanges();
    }
}