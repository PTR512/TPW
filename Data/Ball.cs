using System.Reflection;

namespace Data
{
    internal class Ball
    {
        private int x;
        private int y;
        private int diameter;
        private int xSpeed;
        private int ySpeed;

        public Ball(int x, int y, int diameter, int xSpeed, int ySpeed)
        {
            this.x = x;
            this.y = y;
            this.diameter = diameter;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }

        public int XSpeed
        {
            get { return xSpeed; }
            set { xSpeed = value; }
        }

        public int YSpeed
        {
            get { return ySpeed; }
            set { ySpeed = value; }
        }

      
    }
}
