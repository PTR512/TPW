
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract void CreateBalls(int amount);
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        public abstract List<IBallPosition> GetBalls();

        
        public static LogicAPI CreateInstance(DataAPI? Data = default, List<IBall>? Balls = default)
        {
            return new BallManager(Data ?? DataAPI.CreateInstance(), Balls ?? new List<IBall>());
        }

    }
}
