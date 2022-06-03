using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Providers;

public class CustomUserIdProvider : IUserIdProvider
{
    private readonly IMediator _mediator;

    public string GetUserId(HubConnectionContext connection)
    {
        var jwtToken = connection.GetHttpContext()?.Request.Headers.Authorization;
        var userCredentials = new AuthenticationCredentials(jwtToken);
        var userModelQuery = new GetUserModel.Query(userCredentials);
        var user = _mediator.Send(userModelQuery);

        var userModel = user.Result;
        return userModel.UserModel.Id.ToString();
    }
}