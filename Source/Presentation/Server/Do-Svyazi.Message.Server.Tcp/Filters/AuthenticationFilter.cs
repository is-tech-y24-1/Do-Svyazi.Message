using System.Security.Claims;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class AuthenticationFilter : IHubFilter
{
    private readonly IMediator _mediator;

    public AuthenticationFilter(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var userClaim = context.Context.User.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();
        
        var authenticationCredentials = new AuthenticationCredentials(userClaim);

        var response = await _mediator.Send(new GetUserModel.Query(authenticationCredentials));

        var user = response.UserModel;

        context.Context.Items["User"] = user;

        await next(context);
    }
}