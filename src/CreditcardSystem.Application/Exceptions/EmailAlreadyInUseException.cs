namespace CreditcardSystem.Application.Exceptions;

public class EmailAlreadyInUseException : BaseException
{
    public EmailAlreadyInUseException(string message, ExceptionType type)
        : base(message, type) { }

    public EmailAlreadyInUseException(
        string message,
        IEnumerable<String> errors,
        ExceptionType type
    )
        : base(message, errors, type) { }
}
