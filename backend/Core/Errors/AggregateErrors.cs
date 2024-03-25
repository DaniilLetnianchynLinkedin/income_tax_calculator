using Core.Abstractions.Errors;

namespace Core.Errors;

public static partial class Errors
{
    public static class Aggregate
    {
        public static readonly IError InternalServerError = new Error("Internal server error");
        public static readonly IError ValidationError = new Error("Validation failed");
        public static readonly IError IncomingDtoValidationError = new Error("Wrong format of incoming DTO");
    }
}