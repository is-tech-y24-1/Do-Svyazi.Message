using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Providers;

public class UserIdProvider : IUserIdProvider
{
    private IMediator _mediator;
    public string? GetUserId(HubConnectionContext connection)
    {
        var response = _mediator.Send(new GetUserModel.Query(new AuthenticationCredentials(connection.User.Identity.Name)));
        var userId = response.Result.UserModel.Id.ToString();
        return userId;
    }
}