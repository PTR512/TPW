using System.Numerics;

namespace Data.Abstract
{
    public abstract class IBall
    {
        public abstract event EventHandler? ChangedPosition;
        public abstract void ChangeSpeed(Vector2 speed);
        public abstract void StopBall();
        public abstract void LetBallMove();
        // provides X, Y, speedX, speedY
        public abstract Vector2 getSpeed();
        public static IBall CreateInstance(Vector2 position, Vector2 speed, bool isRunning)
        // provides X, Y, speedX, speedY
        public abstract Vector2 getSpeed();
        // provides X, Y, speedX, speedY
        public abstract Vector2 getSpeed();
        // provides X, Y, speedX, speedY
        public abstract Vector4 getPositionAndSpeed();
        public abstract float getMass();
        public static IBall CreateInstance(Vector2 position, float radius, Vector2 speed, bool isRunning, float mass)
        {
            return new Ball(position, speed, isRunning);
        }


    }
}
