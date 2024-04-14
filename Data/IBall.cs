using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract void ChangeSpeed(float xSpeed, float ySpeed);
        public abstract void StopBall();
        public abstract void LetBallMove();
        public abstract (float, float) getPosition();
        public abstract (float, float) getSpeed();
        public static IBall CreateInstance(float x, float y, float radius, float xSpeed, float ySpeed, bool isRunning)
        {
            return new Ball(x, y, radius, xSpeed, ySpeed, isRunning);
        }
        
      
    }
}
