namespace ULW.Views;

public class MenuView
{
    public void DisplayErrorMessage(string errorMessage)
    {
        Console.WriteLine($"ERROR: {errorMessage}");
    }

    public void DisplayMenu(List<string> subjectsNames)
    {
        Console.WriteLine("----------Menu----------");

        for (int i = 0; i < subjectsNames.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {subjectsNames[i]}");
        }
        
        Console.WriteLine($"\n{subjectsNames.Count + 1}. Close application");

        Console.WriteLine("--------End-Menu--------");
    }

    public string GetUserInputString()
    {
        Console.Write("Enter the action number: ");
        return Console.ReadLine() ?? "";
    }
}