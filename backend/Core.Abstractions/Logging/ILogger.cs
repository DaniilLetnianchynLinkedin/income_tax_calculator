namespace Core.Abstractions.Logging;

public interface ILogger
{
    void Error(Exception exception, string traceId);

    void Error(string message, string traceId);

    void Info(string message, string traceId);

    void Debug(Exception exception, string traceId);

    void Warning(string message, string traceId);
}
