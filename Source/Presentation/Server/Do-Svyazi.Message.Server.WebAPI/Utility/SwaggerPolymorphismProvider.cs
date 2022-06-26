using System.Reflection;

namespace Do_Svyazi.Message.Server.WebAPI.Utility;

public class SwaggerPolymorphismProvider
{
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public void Add(Assembly assembly)
        => _assemblies.Add(assembly);

    public void Add(params Type[] types)
        => _assemblies.AddRange(types.Select(t => t.Assembly));

    public IEnumerable<Type> Resolve(Type baseType)
    {
        return _assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(baseType));
    }
}