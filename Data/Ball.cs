using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Data
{
    internal class Ball : Abstract.IBall
    {
        private Vector2 position;
        private Vector2 speed;
        private bool isRunning;
        private int id;
        public override event EventHandler? ChangedPosition;

        public Ball(Vector2 position, Vector2 speed, bool isRunning, int id)
        {
            this.position = position;
            this.speed = speed;
            this.isRunning = isRunning;
            this.id = id;
            new Thread(new ThreadStart(Move)).Start();


        }
        public void OnChangedPosition()
        {
            ChangedPosition?.Invoke(this, new EventArgs());
        }
        public override void ChangeSpeed(Vector2 speed)
        {

            this.speed = speed;
        }


        private async void Move()
        {

            Stopwatch stopWatch = new Stopwatch();
            float multiplier = 1;
            double start;
            double end;
            stopWatch.Start();
            while (true)
            {
                start = stopWatch.Elapsed.TotalMilliseconds;
                if (isRunning)
                {
                    position += speed * multiplier;
                    OnChangedPosition();
                    Vector4 ballInfo = this.getPositionAndSpeed();
                    Logger.logBallInfo(id, ballInfo);
                }
                await Task.Delay(5);
                
                end = stopWatch.Elapsed.TotalMilliseconds;
                multiplier = (float) ((end - start) / 5);
            }

        }

        public override void StopBall()
        {
            isRunning = false;
        }
        public override void LetBallMove()
        {
            isRunning = true;
        }

        public override Vector4 getPositionAndSpeed()
        {
            return new Vector4(position.X, position.Y, speed.X, speed.Y);
        }
    }
}
