
namespace WebAppWare.Api.Middleware;

public class ErrorHandlingService : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NoContentException ex)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InvalidDataException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (SqlTransactionFailedException ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
