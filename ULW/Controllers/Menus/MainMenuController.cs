using System.Reflection;
using ULW.Views;

namespace ULW.Controllers.Menus;

public class MainMenuController
{
    private List<Type> _entryControllers;
    private MenuView _menuView;

    /// <summary>
    /// Number that indicate selected action in menu. (Values: 1 - actionListSize; + 1 (exit/back func))
    /// </summary>
    private int _selectedNumber;
    /// <summary>
    /// Number of action
    /// </summary>
    private int _menuItemsCount;
    
    private string? ErrorMessage { get; set; } = null;

    private const string _incorrectInputMessage = "Incorrect input, please try again!";

    public MainMenuController(List<Type> entryControllers)
    {
        _entryControllers = entryControllers;

        _menuView = new MenuView();
    }

    public void DisplayMainMenu()
    {
        while (true)
        {
            Console.Clear();

            if (ErrorMessage != null)
            {
                _menuView.DisplayErrorMessage(ErrorMessage);

                ErrorMessage = null;
            }

            _menuView.DisplayMenu(GetSubjectsNames());

            if (IsUserStringCorrect(_menuView.GetUserInputString()))
            {
                if (_selectedNumber.Equals(_menuItemsCount + 1))
                {
                    Environment.Exit(0);
                }
                
                SubjectMenuController subjectMenuController = new (_entryControllers[_selectedNumber - 1]);
                
                subjectMenuController.DisplayMainMenu();
            }
            else
            {
                ErrorMessage = _incorrectInputMessage;
            }
        }
    }

    private List<string> GetSubjectsNames()
    {
        List<string> subjectNames = new();

        foreach (var entryController in _entryControllers)
        {
            object instance = Activator.CreateInstance(entryController);
            
            PropertyInfo propertyInfo = entryController.GetProperty("SubjectName") 
                                        ?? throw new Exception("Field with name \"{SubjectName}\" was not found");;

            string fieldValue = propertyInfo.GetValue(instance) as string 
                                ?? throw new Exception("Field value cant be parse to string");
            
            subjectNames.Add(fieldValue);
        }

        _menuItemsCount = subjectNames.Count;

        return subjectNames;
    }

    private bool IsUserStringCorrect(string userInputString)
    {
        if (int.TryParse(userInputString, out int userInputNumber)) // Check that string is int
        {
            if (userInputNumber > 0 && userInputNumber <= _menuItemsCount + 1)
            {
                _selectedNumber = userInputNumber;
                return true;
            } // Check that int is an action number ("+1" for exit action)
        }

        return false;
    }
}