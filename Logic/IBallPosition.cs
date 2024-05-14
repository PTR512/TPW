using Data.Abstract;
using System.ComponentModel;

namespace Logic.Abstract
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
