using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;

namespace Do_Svyazi.Message.Integrations.FakeUser;

public class FakeAuthenticationService : IAuthenticationService
{
    public Task<UserModel> AuthenticateAsync(AuthenticationCredentials credentials, CancellationToken cancellationToken)
        => Task.FromResult(new UserModel(Guid.Parse("8637F68A-8FC9-43A2-83F6-6156128BA576")));
}