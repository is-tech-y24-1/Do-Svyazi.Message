using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Do_Svyazi.Message.Server.Http.Middlewares;

public class AuthenticationMiddleware
{
    private IMediator _mediator;
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context, IMediator mediator)
    {
        _mediator = mediator;
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachUserToContext(context, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        var authenticationCredentials = new AuthenticationCredentials(token);

        var response = _mediator.Send(new GetUserModel.Query(authenticationCredentials));

        var user = response.Result.UserModel;

        context.Items["User"] = user;
    }
}