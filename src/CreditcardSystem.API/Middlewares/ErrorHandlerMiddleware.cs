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
            context.Response.StatusCode = exception.Type == ExceptionType.Validation ? 404 : 500;
            await context.Response.WriteAsJsonAsync(
                new
                {
                    Success = false,
                    Message = exception.Message,
                    Errors = exception.Errors
                }
            );
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { Message = exception.Message });
        }
    }
}
