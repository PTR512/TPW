using System.Windows.Input;

namespace ViewModel
{
    internal class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }

        public Command(Action action)
        {
            this.action = action;
        }
    }
}
