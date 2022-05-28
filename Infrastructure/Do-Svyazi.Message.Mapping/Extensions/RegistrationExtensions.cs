using Microsoft.Extensions.DependencyInjection;

namespace Do_Svyazi.Message.Mapping.Extensions;

public static class RegistrationExtensions
{
    public static void AddMapper(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(IAssemblyMarker));
    }
}