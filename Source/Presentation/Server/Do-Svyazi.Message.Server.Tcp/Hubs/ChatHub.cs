using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    public async Task Enter(ChatUserDto chatUser)
    {
        if (string.IsNullOrEmpty(username))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
        }
    }

    public async Task Send(string message, string username)
    {
        await Clients.Group(groupname).SendAsync("Receive", message, username);
    }
}