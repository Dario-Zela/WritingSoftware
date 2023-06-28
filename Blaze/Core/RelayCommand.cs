using System;
using System.Windows.Input;

namespace Blaze.Core
{
    /// <summary>
    /// ICommand abstraction, allows for simpler to read code
    /// </summary>
    internal class RelayCommand : ICommand
    {
        //Action to execute and contion on it executing
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        //Event to handle changing execution status
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Constructor
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        //Check if execution is possible
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        //Executes action
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
