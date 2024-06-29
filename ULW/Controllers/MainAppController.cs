using System.Reflection;
using ULW.Controllers.Menus;

namespace ULW.Controllers;

public class MainAppController
{
    public static List<Assembly> Assemblies { get; set; }

    static MainAppController()
    {
        AssemblyController assemblyController = new();

        Assemblies = assemblyController.LoadAssemblies();
    }

    private List<Type> GetEntryControllers()
    {
        List<Type> entryControllerBases = new();
        
        foreach (var assembly in Assemblies)
        {
            Type entryControllerType = assembly.GetType($"{assembly.GetName().Name}.Controllers.EntryController") 
                                       ?? throw new Exception("Error in getting entry type");

            entryControllerBases.Add(entryControllerType);
        }
    
        return entryControllerBases;
    }

    public void StartApp()
    {
        MainMenuController mainMenuController = new MainMenuController(GetEntryControllers());

        mainMenuController.DisplayMainMenu();
    }
}