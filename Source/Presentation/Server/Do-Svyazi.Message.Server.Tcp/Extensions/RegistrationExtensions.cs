using Do_Svyazi.Message.Server.Tcp.Filters;
using Do_Svyazi.Message.Server.Tcp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Server.Tcp.Extensions;

public static class RegistrationExtensions
{
    public static void AddTcpServer(this IServiceCollection collection)
    {
        collection.AddSignalR(c => c.AddFilter<ExceptionFilter>());
    }

    public static void UseTcpServer(this IApplicationBuilder builder)
    {
        builder.UseEndpoints(e =>
        {
            e.MapHub<ChatHub>("/chats");
        });
    }
}