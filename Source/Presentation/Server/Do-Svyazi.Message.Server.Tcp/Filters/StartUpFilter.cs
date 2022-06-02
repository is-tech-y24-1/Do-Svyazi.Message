using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Server.Tcp.Providers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class StartUpFilter : IHubFilter
{
    private Mediator _mediator;

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var jwtToken = context.Context.GetHttpContext()?.Request.Headers.Authorization;
        var userModel = _mediator.Send(new GetUserModel.Query(new AuthenticationCredentials(jwtToken)));
        context.Context.Items.Add(userModel, userModel);
        return next(context);
    }
}