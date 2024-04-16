
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        // create balls with random position and speed
        public abstract void CreateBalls(int amount);
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        // return a list of wrapped balls stripped from the ability to change the state of the original balls
        public abstract List<IBallPosition> GetBalls();

        // static method that creates an instance of LogicAPI with the ability to inject DataAPI and list of balls
        public static LogicAPI CreateInstance(DataAPI? Data = default, List<IBall>? Balls = default)
        {
            return new BallManager(Data ?? DataAPI.CreateInstance(), Balls ?? new List<IBall>());
        }

    }
}
