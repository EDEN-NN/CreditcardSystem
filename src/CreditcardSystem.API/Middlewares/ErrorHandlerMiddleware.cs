using System.Reflection.Metadata.Ecma335;
using CreditcardSystem.Application.Exceptions;

namespace CreditcardSystem.API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorHandlerMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (BaseException exception)
        {
            switch (exception.Type)
            {
                case ExceptionType.NotFoundException:
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            Success = false,
                            Message = exception.Message,
                            Erros = exception.Errors
                        }
                    );
                    break;
                case ExceptionType.EmailAlreadyInUseException:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            Success = false,
                            Message = exception.Message,
                            Erros = exception.Errors
                        }
                    );
                    break;
                case ExceptionType.UserAlreadyExists:
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            Success = false,
                            Message = exception.Message,
                            Erros = exception.Errors
                        }
                    );
                    break;
                case ExceptionType.Validation:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            Success = false,
                            Message = exception.Message,
                            Erros = exception.Errors
                        }
                    );
                    break;
            }
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { Message = exception.Message });
        }
    }
}
