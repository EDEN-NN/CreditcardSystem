namespace CreditcardSystem.Application.Exceptions;

public class UserNotFoundException : BaseException
{
    public UserNotFoundException(string message, ExceptionType type)
        : base(message, type) { }

    public UserNotFoundException(string message, IEnumerable<String> errors, ExceptionType type)
        : base(message, errors, type) { }
}
