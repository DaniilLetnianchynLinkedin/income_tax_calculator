namespace Core.Errors;

public record InternalServerError(string Message) : Error(Message);