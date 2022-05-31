using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;

namespace Do_Svyazi.Message.Application.Abstractions.Integrations;

public interface IAuthenticationService
{
    Task<UserModel> AuthenticateAsync(AuthenticationCredentials credentials, CancellationToken cancellationToken);
}