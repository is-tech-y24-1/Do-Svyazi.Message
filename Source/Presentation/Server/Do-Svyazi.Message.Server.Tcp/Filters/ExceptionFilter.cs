using Do_Svyazi.Message.Application.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class ExceptionFilter : IHubFilter
{
    public async Task InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            await next(invocationContext);
        }
        catch (NotFoundException e)
        {
            var httpContext = invocationContext.Context.GetHttpContext();
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            httpContext.Response.ContentType = "text/*";

            await httpContext.Response.WriteAsync(e.Message);
        }
        catch (InvalidRequestException e)
        {
            var httpContext = invocationContext.Context.GetHttpContext();
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "text/*";

            await httpContext.Response.WriteAsync(e.Message);
        }
        catch (UnauthorizedException e)
        {
            var httpContext = invocationContext.Context.GetHttpContext();
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            httpContext.Response.ContentType = "text/*";

            await httpContext.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            var httpContext = invocationContext.Context.GetHttpContext();
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "text/*";

            await httpContext.Response.WriteAsync(e.Message);
        }
    }
}