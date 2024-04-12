using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    internal class ViewModel : ViewModelAPI
    {
        ModelAPI modelAPI = ModelAPI.CreateInstance();
        private ObservableCollection<object> _balls;
        public ViewModel() {_balls = modelAPI.GetBalls();}
        public override ObservableCollection<object> Balls { get => _balls; set => _balls = value; }
    }
}
