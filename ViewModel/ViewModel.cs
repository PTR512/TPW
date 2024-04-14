using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        ModelAPI modelAPI = ModelAPI.CreateInstance();
        private ObservableCollection<object> _balls;

        //Tak było w tutorialu
        public event PropertyChangedEventHandler? PropertyChanged;
    
        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public ObservableCollection<object> Balls
        { 
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged();
            }
        }
        public int BallAmount 
        { 
            get => modelAPI.BallAmount;
            set
            {
                modelAPI.BallAmount = value; 
                OnPropertyChanged();
            }
        }

        public ICommand IncreaseBallAmount { get; }

        public ICommand DecreaseBallAmount { get; }

        public void DecreaseAmount()
        {
            if (BallAmount > 0) BallAmount -= 1;
            System.Diagnostics.Debug.WriteLine(BallAmount);
        }

        public void IncreaseAmount()
        {
            if (BallAmount < 15) BallAmount += 1;
            System.Diagnostics.Debug.WriteLine(BallAmount);
        }

        public ViewModel()
        {
            _balls = modelAPI.GetBalls();
            IncreaseBallAmount = new Command(IncreaseAmount);
            DecreaseBallAmount = new Command(DecreaseAmount);
        }
    }
}
