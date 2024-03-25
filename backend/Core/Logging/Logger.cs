using ILogger = Core.Abstractions.Logging.ILogger;
using ISerilogLogger = Serilog.ILogger;

namespace Core.Logging;

public class Logger : ILogger
{
    private readonly ISerilogLogger serilogLogger;

    public Logger(ISerilogLogger logger)
    {
        this.serilogLogger = logger;
    }

    public void Error(Exception exception, string traceId)
    {
        serilogLogger.Error(exception, exception.Message, traceId);
    }

    public void Error(string message, string traceId)
    {
        serilogLogger.Error(message, traceId);
    }

    public void Info(string message, string traceId)
    {
        serilogLogger.Information(message, traceId);
    }

    public void Debug(Exception exception, string traceId)
    {
        serilogLogger.Debug(exception, exception.Message, traceId);
    }

    public void Warning(string message, string traceId)
    {
        serilogLogger.Warning(message, traceId);
    }
}
