using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall
    {

        public abstract void ChangeSpeed(float xSpeed, float ySpeed);
        public static IBall CreateInstance(float x, float y, float radius, float xSpeed, float ySpeed)
        {
            return new Ball(x, y, radius, xSpeed, ySpeed);
        }
        
      
    }
}
