using System.Reflection;

namespace Logic
{
    internal class Ball
    {
        private float x;
        private float y;
        private float radius;
        private float xSpeed;
        private float ySpeed;

        public Ball(float x, float y, float radius, float xSpeed, float ySpeed)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
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
        public void Move(int xBoundary, int yBoundary)
        {
            if (xSpeed == 0 &&  ySpeed == 0) return;
            if (!withinBoundaries(x + xSpeed, xBoundary))
            {
                xSpeed = -xSpeed;
            }
            if (!withinBoundaries(y + ySpeed, yBoundary))
            {
                ySpeed = -ySpeed;
            }
            x += xSpeed;
            y += ySpeed;
        }
        private bool withinBoundaries(double position, int boundary) // on one axis
        {
            return 0 <= (position - radius) && (position + radius) <= boundary;
        }
      
    }
}
