using System.Windows.Input;

namespace ViewModel
{
    internal class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
                return true;
            if (parameter == null)
                return this._canExecute();
            return this._canExecute();
        }

        public void Execute(object parameter)
        {
            this._execute();
        }

        public Command(Action execute, Func<bool> canExecute = null)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
