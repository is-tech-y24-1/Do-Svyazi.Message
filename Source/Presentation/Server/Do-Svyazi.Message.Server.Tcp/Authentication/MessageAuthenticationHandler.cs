using System.Security.Claims;
using System.Text.Encodings.Web;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Do_Svyazi.Message.Server.Tcp.Authentication;

public class MessageAuthenticationHandler : AuthenticationHandler<ChatAuthSchemeOptions>
{
    private readonly IMediator _mediator;

    public MessageAuthenticationHandler(
        IOptionsMonitor<ChatAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IMediator mediator) : base(options, logger, encoder, clock)
    {
        _mediator = mediator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token))
        {
            var message = "Token is missing";
            return AuthenticateResult.Fail(message);
        }

        var userId = await GetUserModelId(token);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var identity = new ClaimsIdentity(claims, nameof(MessageAuthenticationHandler));
        var identityPrincipal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(
            identityPrincipal, Scheme.Name
        );

        return AuthenticateResult.Success(ticket);
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