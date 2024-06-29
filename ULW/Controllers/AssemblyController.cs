using System.Diagnostics;
using System.Reflection;

namespace ULW.Controllers;

public class AssemblyController
{
    private readonly string _assembliesDirectoryPath = @"Assemblies\";

    public List<Assembly> LoadAssemblies()
    {
        List<Assembly> assemblies = new();

        foreach (var file in Directory.GetFiles(_assembliesDirectoryPath, "*.dll"))
        {
            try
            {
                assemblies.Add(Assembly.LoadFrom(file));
#if DEBUG
                Console.WriteLine($"Loaded assembly: {assemblies[^1].FullName}");
                Console.ReadKey();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading assembly: {file} - {ex.Message}");
            }
        }

        return assemblies;
    }
}