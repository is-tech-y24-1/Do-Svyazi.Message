using Do_Svyazi.Message.Application.Abstractions.Exceptions;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using ApplicationException = System.ApplicationException;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class CustomFilter : IHubFilter
{
    private IMediator _mediator;

    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext context, Func<HubInvocationContext, ValueTask<object>> next)
    {
        try
        {
            return await next(context);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (InvalidRequestException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (UnauthorizedException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var userJwtToken = context.Context.UserIdentifier;
        var response = _mediator.Send(new GetUserModel.Query(new AuthenticationCredentials(userJwtToken)));
        context.Context.Items.Add(userJwtToken, response); // че ?
        return response;
    }
}