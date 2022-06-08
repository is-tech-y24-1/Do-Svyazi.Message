using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Providers;

public class ChatUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();
    }
}