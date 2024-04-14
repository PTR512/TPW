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
        
        public ICommand Start {  get; }
        public ICommand Stop { get; }

        public void DecreaseAmount()
        {
            if (BallAmount > 0) BallAmount -= 1;
            System.Diagnostics.Debug.WriteLine(modelAPI.BallAmount);
        }

        public void IncreaseAmount()
        {
            if (BallAmount < 15) BallAmount += 1;
            System.Diagnostics.Debug.WriteLine(BallAmount);
            System.Diagnostics.Debug.WriteLine("BALLS: "+Balls.Count);
        }

        public void RunSimulation()
        {
            modelAPI.CreateBalls();
            Balls = modelAPI.GetBalls();
            modelAPI.Start();
        }

        public ViewModel()
        {
            Balls = modelAPI.GetBalls();
            IncreaseBallAmount = new Command(IncreaseAmount);
            DecreaseBallAmount = new Command(DecreaseAmount);
            Start = new Command(RunSimulation);
            Stop = new Command(modelAPI.Stop);

        }
    }
}
