namespace Core.Errors.Validation;

public record ValidationError(string Message) : Error(Message);