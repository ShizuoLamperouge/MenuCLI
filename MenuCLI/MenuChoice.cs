using System.Diagnostics;
using System.Reflection;

namespace MenuCLI
{
    internal class MenuChoice
    {
        private string _displayString;

        private Func<Task>? _asyncCallback;

        private Action? _callback;

        private bool _waitForUserInput;

        private IConsoleWrapper _consoleWrapper;

        internal MenuChoice(string displayString, Func<Task> asyncCallback, IConsoleWrapper consoleWrapper, bool waitForUserInput= true)
        {
            _displayString = displayString;
            _asyncCallback = asyncCallback;
            _waitForUserInput = waitForUserInput;
            _consoleWrapper = consoleWrapper;
        }

        internal MenuChoice(string displayString, Action action, IConsoleWrapper consoleWrapper, bool waitForUserInput = true)
        {
            _displayString = displayString;
            _callback = action;
            _waitForUserInput= waitForUserInput;
            _consoleWrapper = consoleWrapper;
        }

        internal void Display(int index)
        {
            Console.WriteLine($"    {index + 1}. {_displayString}");
            Console.WriteLine();
        }

        internal async Task Run()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine(_displayString);
            Console.WriteLine();

            if (_asyncCallback != null)
            {
                await _asyncCallback.Invoke();
            } 
            else if (_callback != null)
            {
                _callback();
            }
            else
            {
                throw new ArgumentNullException($"The choice need to have a callback to be executed");
            }

            if (_waitForUserInput)
            {
                Console.WriteLine();
                Console.WriteLine("Press a key to continue...");
                _consoleWrapper.WaitForUserKeyPress();
            }
        }
    }
}