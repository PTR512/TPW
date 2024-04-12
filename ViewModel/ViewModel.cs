using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    internal class ViewModel : ViewModelAPI
    {
        ModelAPI modelAPI = ModelAPI.CreateInstance();
        private ObservableCollection<object> _balls;
        public override ObservableCollection<object> Balls { get => _balls; set => _balls = value; }
        public override int BallAmount { get => modelAPI.BallAmount; set => modelAPI.BallAmount = value; }

        public override ICommand IncreaseBallAmount { get; }

        public override ICommand DecreaseBallAmount { get; }

        public override void DecreaseAmount()
        {
            if (BallAmount > 0) BallAmount -= 1;
            System.Diagnostics.Debug.WriteLine(BallAmount);
        }

        public override void IncreaseAmount()
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
