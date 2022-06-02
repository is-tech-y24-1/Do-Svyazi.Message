using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class CustomFilter : IHubFilter
{
    private IMediator _mediator;
    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext context, Func<HubInvocationContext, ValueTask<object>> next)
    {
        return await next(context);
    }

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var userName = context.Context.User?.Identity?.Name;
        var response = _mediator.Send(new GetUserModel.Query(new AuthenticationCredentials(userName)));
        context.Context.Items.Add(userName, response); // че ?
        return response;
    }
}