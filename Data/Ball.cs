using System.ComponentModel;
using System.Reflection;

namespace Data
{
    internal class Ball(float x, float y, float radius, float xSpeed, float ySpeed) : IBall
    {
        private float x = x;
        private float y = y;
        private float radius = radius;
        private float xSpeed = xSpeed;
        private float ySpeed = ySpeed;

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
            x += xSpeed;
            y += ySpeed;
        }
        
      
    }
}
