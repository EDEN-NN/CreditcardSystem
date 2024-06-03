namespace CreditcardSystem.Application.Exceptions;

public class UnauthorizedTokenException : BaseException
{
    public UnauthorizedTokenException(string message, ExceptionType type)
        : base(message, type) { }

    public UnauthorizedTokenException(
        string message,
        IEnumerable<String> errors,
        ExceptionType type
    )
        : base(message, errors, type) { }
}
