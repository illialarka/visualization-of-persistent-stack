using System;
using System.Windows.Input;

namespace PersistentStackVisualization.Command
{
    /// <summary>
    /// Command for push new value into persistent stack
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Execute command
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Availability of execution
        /// </summary>
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        /// <summary>
        /// Constructor for command
        /// </summary>
        /// <param name="execute"> Delegate execute </param>
        /// <param name="canExecute"> Deleget availability of execution </param>
        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Availability of execution
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns> Can or can not execute </returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        /// <summary>
        /// Execution command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}
