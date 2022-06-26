using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Application.CQRS.Extensions;

public static class RegistrationExtensions
{
    public static void AddCqrs(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(IAssemblyMarker));
    }
}