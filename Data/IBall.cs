using System.Numerics;

namespace Data.Abstract
{
    public abstract class IBall
    {
        public abstract event EventHandler? ChangedPosition;
        public abstract void ChangeSpeed(Vector2 speed);
        public abstract void StopBall();
        public abstract void LetBallMove();
        public abstract Vector2 getPosition();
        public abstract Vector2 getSpeed();
        // provides X, Y, speedX, speedY
        public abstract Vector4 getPositionAndSpeed();
        public abstract float getMass();
        public static IBall CreateInstance(Vector2 position, float radius, Vector2 speed, bool isRunning, float mass)
        {
            return new Ball(position, radius, speed, isRunning, mass);
        }


    }
}
