namespace CreditcardSystem.Application.Exceptions;

public class BaseException : Exception
{
    public IEnumerable<String> Errors { get; init; }
    public ExceptionType Type { get; init; }

    public BaseException(string message, IEnumerable<String> errors, ExceptionType type)
        : base(message)
    {
        Errors = errors;
        Type = type;
    }

    public BaseException(string message, ExceptionType type)
        : base(message)
    {
        Type = type;
    }
}

public enum ExceptionType
{
    EmailAlreadyInUseException,
    NotFoundException,
    UserAlreadyExists,
    Validation,
    Exception
}
