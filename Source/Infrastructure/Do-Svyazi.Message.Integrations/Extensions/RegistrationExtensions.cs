using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Integrations.FakeUser;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Integrations.Extensions;

public static class RegistrationExtensions
{
    public static void AddIntegrations(this IServiceCollection collection)
    {
        collection.AddScoped<IAuthenticationService, FakeAuthenticationService>();
        collection.AddScoped<IAuthorizationService, FakeAuthorizationService>();
        collection.AddScoped<IUserModuleService, FakeUserModuleService>();
    }
}