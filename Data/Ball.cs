using System.ComponentModel;
using System.Reflection;

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
        public Ball(float x, float y, float radius, float xSpeed, float ySpeed, bool isRunning)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.isRunning = isRunning;
            Task.Run(Move);
        }
        
        public float X
        {
            get { return x; }
            
        }

        public float Y
        {
            get { return y; }
            
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

        
        private void Move()
        {  
            while (isRunning)
            {
                x += xSpeed;
                y += ySpeed;
                System.Diagnostics.Debug.WriteLine(x + " " + y);
                Thread.Sleep(1000);
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
        
    }
}
