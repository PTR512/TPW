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
        public float X
        {
            get { return x; }
            private set
            {
                x = value;

            }
        }

        public float Y
        {
            get { return y; }
            private set
            {
                y = value;

            }
        }

        public float Radius
        {
            get { return radius; }

        }

        public float XSpeed
        {
            get { return xSpeed; }
            set { xSpeed = value; }
        }

        public float YSpeed
        {
            get { return ySpeed; }
            set { ySpeed = value; }
        }

        public float Mass
        { 
            get { return mass; } 
            set {  mass = value; }
        }


        public override void ChangeSpeed(float xSpeed, float ySpeed)
        {

            this.XSpeed = xSpeed;
            this.YSpeed = ySpeed;
        }


        private async void Move()
        {
            while (true)
            {
                while (isRunning)
                {
                    X += xSpeed;
                    Y += ySpeed;
                    OnChangedPosition();
                    await Task.Delay(5);
                }
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
            return (X, Y);
        }

        public override (float, float) getSpeed()
        {
            return (xSpeed, ySpeed);
        }

        public override float getMass()
        { return mass; }
    }
}
