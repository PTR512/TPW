using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class IBallPosition : INotifyPropertyChanged
    {
        // wrapper for balls
        public abstract event PropertyChangedEventHandler? PropertyChanged;
        public static IBallPosition CreateInstance(IBall Ball)
        {
            return new BallPosition(Ball);
        }
    }
}
