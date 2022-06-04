using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class ExceptionFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            return await next.Invoke(invocationContext);
        }
        catch (Exception e)
        {
            var client = invocationContext.Hub.Clients.Caller;

            await client.SendAsync("ReceiveAsync", e.Message);
        }

        return ValueTask.CompletedTask;
    }
}