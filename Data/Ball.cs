using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class Ball : IBall
    {
        private float x;
        private float y;
        private float radius;
        private float xSpeed;
        private float ySpeed;
        private bool isRunning;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public Ball(float x, float y, float radius, float xSpeed, float ySpeed, bool isRunning)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.isRunning = isRunning;
            System.Diagnostics.Debug.WriteLine("task run");
            Task.Run(Move);
        }
        public void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
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
                x = value;
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


        public override void ChangeSpeed(float xSpeed, float ySpeed)
        {

            this.XSpeed = xSpeed;
            this.YSpeed = ySpeed;
        }

        
        private async void Move()
        {
            
            while (isRunning)
            {
                System.Diagnostics.Debug.WriteLine("Ball is running " + X + " xspeed: "+XSpeed + " " + Y + " y speed: " + YSpeed);
                x += xSpeed;
                y += ySpeed;
                OnPropertyChanged();
                await Task.Delay(1000);
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
    }
}
