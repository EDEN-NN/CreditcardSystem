namespace CreditcardSystem.Application.Exceptions;

public class CreditcardNotFoundException : BaseException
{
    public CreditcardNotFoundException(string message, ExceptionType type)
        : base(message, type) { }

    public CreditcardNotFoundException(
        string message,
        IEnumerable<String> errors,
        ExceptionType type
    )
        : base(message, errors, type) { }
}
