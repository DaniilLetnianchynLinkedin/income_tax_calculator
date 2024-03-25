namespace Core.Errors;

public record AggregateError(string Message) : Error(Message);