
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract void CreateBalls(int amount);
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        public abstract List<IBall> Balls { get; }

        
        public static LogicAPI CreateInstance(DataAPI? data = default)
        {
            return new BallManager(data ?? DataAPI.CreateInstance());
        }

    }
}
