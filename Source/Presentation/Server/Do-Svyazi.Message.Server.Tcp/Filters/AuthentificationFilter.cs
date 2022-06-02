using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Server.Tcp.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class AuthentificationFilter : IHubFilter
{
    private readonly Mediator _mediator;

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var jwtToken = context.Context.GetHttpContext()?.Request.Headers.Authorization;
        var userCredentials = new AuthenticationCredentials(jwtToken);
        var userModelQuery = new GetUserModel.Query(userCredentials);
        var userModel = _mediator.Send(userModelQuery);

        context.Context.Items.Add(userModel, userModel);
        return next(context);
    }
}