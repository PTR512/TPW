using Data;
using System.ComponentModel;

namespace Logic
{
    public abstract class IBallPosition : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;
        public static IBallPosition CreateInstance(IBall Ball)
        {
            return new BallPosition(Ball);
        }
    }
}
