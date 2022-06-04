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
        var token = context.Context.GetHttpContext()?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null)
            await AttachUserToContext(context.Context.GetHttpContext(), token, _mediator);
        
        await next(context);
    }
    
    private static async Task AttachUserToContext(HttpContext context, string token, IMediator mediator)
    {
        var authenticationCredentials = new AuthenticationCredentials(token);

        var response = await mediator.Send(new GetUserModel.Query(authenticationCredentials));

        var user = response.UserModel;

        context.Items["User"] = user;
    }
}