using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Users.Queries;

public static class GetUserModel
{
    public record Query(AuthenticationCredentials Credentials) : IRequest<Response>;

    public record Response(UserModel UserModel);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IAuthenticationService _authenticationService;

        public Handler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var userModel = await _authenticationService.AuthenticateAsync(request.Credentials, cancellationToken);

            return new Response(userModel);
        }
    }
}