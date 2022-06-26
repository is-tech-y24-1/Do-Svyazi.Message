using Do_Svyazi.Message.Application.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Application.Services.Extensions;

public static class RegistrationExtensions
{
    public static void AddApplicationServices(this IServiceCollection collection)
    {
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IChatUserService, ChatUserService>();
        collection.AddSingleton<IDateTimeService, UtcDateTimeService>();
    }
}