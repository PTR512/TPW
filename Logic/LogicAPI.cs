﻿
using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract void CreateBalls(int amount);
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        public abstract List<IBall> Balls { get; }

        public static LogicAPI CreateInstance()
        {
            return new BallManager();
        }
        public static LogicAPI CreateInstance(List<IBall> Balls)
        {
            return new BallManager(Balls);
        }
    }
}
