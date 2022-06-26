using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.DataAccess.Extensions;

public static class RegistrationExtensions
{
    public static void AddDataAccess(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> action)
    {
        collection.AddDbContext<MessageDatabaseContext>(action);
        collection.AddScoped<IMessageDatabaseContext, MessageDatabaseContext>();
    }
}