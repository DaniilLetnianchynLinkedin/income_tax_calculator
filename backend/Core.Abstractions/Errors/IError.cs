namespace Core.Abstractions.Errors;

public interface IError
{
    public string Message { get; }

    public string TypeName { get; }
}
