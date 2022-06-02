using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Providers;

public class CustomUserIdProvider : IUserIdProvider
{
    private Mediator _mediator;

    public string? GetUserId(HubConnectionContext connection)
    {
        var user = _mediator.Send(
            new GetUserModel.Query(
                new AuthenticationCredentials(connection.GetHttpContext().Request.Headers.Authorization)));

        var userModel = user.Result;
        return userModel.UserModel.Id.ToString();
    }
}