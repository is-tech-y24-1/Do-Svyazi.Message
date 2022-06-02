using System.ComponentModel.DataAnnotations.Schema;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub<ChatUserDto>
{
    [CustomFilter]
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
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