namespace tPix.ViewModel.Cmd
{
    using System;
    using System.Windows.Input;

    //TODO, why isn't this generic?
    public class UpdateLocationCmd : ICommand
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ConfigFormCmd"/> class
        /// </summary>
        /// <param name="command">The method to run</param>
        public UpdateLocationCmd(Action command)
        {
            this.RunCommand = command;
        }

        /// <summary>
        /// Gets an <see cref="Action"/> to run.
        /// </summary>
        public Action RunCommand { get; private set; }

        /// <summary>
        /// Indicates whether the command can be run.
        /// </summary>
        /// <param name="parameter">Not used</param>
        /// <returns>Indicates whether the command can be run</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// TODO, to be implemented.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Run the provided command
        /// </summary>
        /// <param name="parameter">unused parameter</param>
        public void Execute(object parameter)
        {
            this.RunCommand();
        }
    }
}