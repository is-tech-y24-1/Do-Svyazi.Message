using Do_Svyazi.Message.Server.WebAPI.Authentication;

namespace Do_Svyazi.Message.Server.WebAPI.Extensions;

public static class RegistrationExtensions
{
    public static void AddCustomAuthentication(this IServiceCollection collection)
    {
        collection.AddAuthentication(o => o.DefaultScheme = nameof(MessageAuthenticationHandler))
            .AddScheme<MessageAuthenticationSchemeOptions, MessageAuthenticationHandler>(
                nameof(MessageAuthenticationHandler), _ => { });
    }
}