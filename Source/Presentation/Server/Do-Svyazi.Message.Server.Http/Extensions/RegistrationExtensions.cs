using System.Text.Json.Serialization;
using Do_Svyazi.Message.Server.Http.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Server.Http.Extensions;

public static class RegistrationExtensions
{
    public static void AddHttpServer(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(IAssemblyMarker).Assembly)
            .AddNewtonsoftJson()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static void UseHttpServer(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();
        builder.UseMiddleware<AuthenticationMiddleware>();
    }
}