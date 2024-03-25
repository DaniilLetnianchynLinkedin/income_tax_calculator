using Newtonsoft.Json;
using Core.Abstractions.Errors;

namespace Core.Errors;

public record Error : IError
{
    public Error(string message)
        : this(message, string.Empty)
    {
        Message = message;
    }

    [JsonConstructor]
    public Error(string message, string typeName)
    {
        Message = message;
        TypeName = string.IsNullOrEmpty(typeName) ? GetType().FullName ?? string.Empty : typeName;
    }

    public string Message { get; }

    public string TypeName { get; }
}
