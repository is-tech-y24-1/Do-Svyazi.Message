using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Application.CQRS.Extensions;

public static class RegistrationExtensions
{
    public static void RegisterCQRS(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(IAssemblyMarker));
    }
}