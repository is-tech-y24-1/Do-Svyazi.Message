using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Providers;

public class ChatUserIdProvider : IUserIdProvider
{
    private readonly IMediator _mediator;

    public string GetUserId(HubConnectionContext connection)
    {
        throw new NotImplementedException();
    }
}