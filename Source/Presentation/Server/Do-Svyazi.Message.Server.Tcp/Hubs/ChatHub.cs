using Do_Svyazi.Message.Application.Dto.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    public async Task SendMessage(MessageDto messageDto)
        => await Clients.All.SendAsync("ReceiveMessage", messageDto);
}