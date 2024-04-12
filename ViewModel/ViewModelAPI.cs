using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class ViewModelAPI
    {
        public abstract ObservableCollection<object> Balls { get; set; }
        public abstract int BallAmount { get; set; }
        public abstract ICommand IncreaseBallAmount { get; }
        public abstract ICommand DecreaseBallAmount { get; }
        public abstract void IncreaseAmount();
        public abstract void DecreaseAmount();
        public static ViewModelAPI CreateInstance()
        {
            return new ViewModel();
        }
    }
}
