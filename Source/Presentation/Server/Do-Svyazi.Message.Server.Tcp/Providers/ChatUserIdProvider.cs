using System.Security.Claims;
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
        return connection.User.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();
    }
}