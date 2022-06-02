using Do_Svyazi.Message.Application.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Do_Svyazi.Message.Server.Http.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "text/*";

            await context.Response.WriteAsync(e.Message);
        }
        catch (InvalidRequestException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "text/*";

            await context.Response.WriteAsync(e.Message);
        }
        catch (UnauthorizedException e)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "text/*";

            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/*";

            await context.Response.WriteAsync(e.Message);
        }
    }
}

