using System.Reflection;
using ULW.Views;

namespace ULW.Controllers.Menus
{
    public class SubjectMenuController
    {
        private Type _subjectEntry;
        private MenuView _menuView;

        private int _selectedNumber;
        private int _menuItemsCount;

        private string? ErrorMessage { get; set; } = null;

        private const string _incorrectInputMessage = "Incorrect input, please try again!";

        public SubjectMenuController(Type subjectEntry)
        {
            _subjectEntry = subjectEntry;

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
                    object instance = Activator.CreateInstance(_subjectEntry)
                                      ?? throw new Exception("Cant create instance of the entry controller");
                    ;


                    MethodInfo methodInfo = _subjectEntry.GetMethod("ExecuteActionByNumber")
                                            ?? throw new Exception("Method not found");

                    object[] parameters = { _selectedNumber };

                    methodInfo.Invoke(instance, parameters);
                }
                else
                {
                    ErrorMessage = _incorrectInputMessage;
                }
            }
        }

        private List<string> GetSubjectsNames()
        {
            object instance = Activator.CreateInstance(_subjectEntry)
                              ?? throw new Exception("Cant create instance of the entry controller");
            ;


            MethodInfo methodInfo = _subjectEntry.GetMethod("GetMenuItemsNamesList")
                                    ?? throw new Exception("Method not found");

            var result = methodInfo.Invoke(instance, null) as List<string>
                         ?? throw new Exception($"Method result cant be parsed to List<string>");

            _menuItemsCount = result.Count;

            return result;
        }

        private bool IsUserStringCorrect(string userInputString)
        {
            if (int.TryParse(userInputString, out int userInputNumber)) // Check that string is int
            {
                if (userInputNumber > 0 && userInputNumber <= _menuItemsCount + 1)
                {
                    _selectedNumber = userInputNumber;
                    return true;
                }
            }

            return false;
        }
    }
}