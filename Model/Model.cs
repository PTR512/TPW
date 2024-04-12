using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class Model : ModelAPI
    {
        private ObservableCollection<object> _balls = new ObservableCollection<object>();
        private LogicAPI logicAPI = LogicAPI.CreateInstance();
        public override void CreateBalls(int amount)
        {
            logicAPI.CreateBalls(amount);
        }

        public override ObservableCollection<object> GetBalls()
        {
            foreach (object ball in logicAPI.Balls)
            {
                _balls.Add(ball);
            }
            return _balls;
        }

        public override void Start()
        {
            logicAPI.RunSimulation();
        }

        public override void Stop()
        {
            logicAPI.StopSimulation();
        }
    }
}
