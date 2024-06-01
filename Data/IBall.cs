using System.Numerics;

namespace Data.Abstract
{
    public abstract class IBall
    {
        public abstract event EventHandler? ChangedPosition;
        public abstract void ChangeSpeed(Vector2 speed);
        public abstract void StopBall();
        public abstract void LetBallMove();
        public abstract Vector4 getPositionAndSpeed();
        public static IBall CreateInstance(Vector2 position, Vector2 speed, bool isRunning)
        {
            return new Ball(position, speed, isRunning);
        }


    }
}
