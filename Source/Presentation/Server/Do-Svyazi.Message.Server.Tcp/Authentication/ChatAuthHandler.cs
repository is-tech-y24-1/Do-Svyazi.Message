using System.Security.Claims;
using System.Text.Encodings.Web;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Do_Svyazi.Message.Server.Tcp.Authentication;

public class ChatAuthHandler : AuthenticationHandler<ChatAuthSchemeOptions>
{
    private readonly IMediator _mediator;

    public ChatAuthHandler(IOptionsMonitor<ChatAuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock, IMediator mediator) : base(options, logger, encoder, clock)
    {
        _mediator = mediator;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var isValid = TryAuthenticate(out AuthenticationTicket ticket, out string message);

        return Task.FromResult(isValid ? AuthenticateResult.Success(ticket) : AuthenticateResult.Fail(message));
    }

    private bool TryAuthenticate(out AuthenticationTicket ticket, out string message)
    {
        message = null;
        ticket = null;

        var token = GetToken();
        var userId = GetUserModelId(token);

        var claims = new[] {new Claim(ClaimTypes.NameIdentifier, userId.Result)}; // не уверен как правильно
        var identity = new ClaimsIdentity(claims, nameof(ChatAuthHandler));
        ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);

        return true;
    }

    private string GetToken()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        return token;
    }

    private async Task<string> GetUserModelId(string token)
    {
        var authenticationCredentials = new AuthenticationCredentials(token);

        var response = await _mediator.Send(new GetUserModel.Query(authenticationCredentials));

        return response.UserModel.Id.ToString();
    }
}