using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class Ball : Abstract.IBall
    {
        private Vector2 position;
        private float radius;
        private Vector2 speed;
        private bool isRunning;
        private float mass;
        public override event EventHandler? ChangedPosition;

        public Ball(Vector2 position, float radius, Vector2 speed, bool isRunning, float mass)
        {
            this.position = position;
            this.radius = radius;
            this.speed = speed;
            this.isRunning = isRunning;
            this.mass = mass;
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
            float multiplier = 0;
            // trzeba uwzglednic czas przy liczeniu predkosci
            while (true)
            {
                
                if (isRunning)
                {
                    position += speed * multiplier;
                    OnChangedPosition();
                }

                //Debug.WriteLine(stopWatch.Elapsed.TotalMilliseconds + " " + multiplier);

                //await Task.Delay(TimeSpan.FromMilliseconds(velocity * (stopWatch.ElapsedMilliseconds + 5) / 5));
                stopWatch.Restart();
                await Task.Delay(5);
                stopWatch.Stop();
                multiplier = ((float) stopWatch.Elapsed.TotalMilliseconds) / 5;
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

        public override Vector2 getPosition()
        {
            return position;
        }

        public override Vector2 getSpeed()
        {
            return speed;
        }

        public override float getMass()
        { return mass; }

        public override Vector4 getPositionAndSpeed()
        {
            return new Vector4(position.X, position.Y, speed.X, speed.Y);
        }
    }
}
