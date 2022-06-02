using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub<ICommunicationHub>
{
    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Identity?.Name;
        var userQuery = GetUserModel.Query(userName);
        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, Context.);
        await base.OnConnectedAsync();
    }

    public async Task SendMessageAsync(MessageDto messageDto)
    {
        await Clients.Others.SendAsync("Receive", messageDto);
    }
    
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName).NotifyAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
    }

    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName).NotifyAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
    }
}