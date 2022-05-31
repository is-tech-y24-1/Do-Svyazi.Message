using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Users.Queries;

public static class GetUserModel
{
    public record Query(AuthenticationCredentials Credentials) : IRequest<Response>;

    public record Response(UserModel UserModel);

    // public class Handler : IRequestHandler<Query, Response>
    // {
    //     public async Task<Response> Handle(Query request, CancellationToken cancellationToken) { }
    // }
}