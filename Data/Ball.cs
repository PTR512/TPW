namespace Data
{
    internal class Ball : Abstract.IBall
    {
        private float x;
        private float y;
        private float radius;
        private float xSpeed;
        private float ySpeed;
        private bool isRunning;
        private float mass;
        public override event EventHandler? ChangedPosition;

        public Ball(float x, float y, float radius, float xSpeed, float ySpeed, bool isRunning, float mass)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.isRunning = isRunning;
            this.mass = mass;
            new Thread(new ThreadStart(Move)).Start();


        }
        public void OnChangedPosition()
        {
            ChangedPosition?.Invoke(this, new EventArgs());
        }
        public override void ChangeSpeed(float xSpeed, float ySpeed)
        {

            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }


        private async void Move()
        {
            // trzeba uwzglednic czas przy liczeniu predkosci
            while (true)
            {
                if (isRunning)
                {
                    x += xSpeed;
                    y += ySpeed;
                    OnChangedPosition();
                }
                await Task.Delay(5);
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

        public override (float, float) getPosition()
        {
            return (x, y);
        }

        public override (float, float) getSpeed()
        {
            return (xSpeed, ySpeed);
        }

        public override float getMass()
        { return mass; }
    }
}
