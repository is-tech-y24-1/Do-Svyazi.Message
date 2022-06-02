using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Do_Svyazi.Message.Server.Http.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context, IMediator mediator)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null)
            AttachUserToContext(context, token, mediator);

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, string token, IMediator mediator)
    {
        var authenticationCredentials = new AuthenticationCredentials(token);

        var response = await mediator.Send(new GetUserModel.Query(authenticationCredentials));

        var user = response.UserModel;

        context.Items["User"] = user;
    }
}