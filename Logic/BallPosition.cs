using Data.Abstract;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class BallPosition : IBallPosition
    {
        private float x, y;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public BallPosition(IBall Ball)
        {
            (float x, float y) = Ball.getPosition();
            X = x;
            Y = y;
            Ball.ChangedPosition += OnChagedPosition;
        }

        private void OnChagedPosition(object? sender, EventArgs e)
        {

            IBall Ball = (IBall) sender;
            (float x, float y) = Ball.getPosition();
            X = x;
            Y = y;
        }

        public float X
        {
            get { return x; }
            private set
            {
                x = value;
                OnPropertyChanged();
            }
        }
        public float Y
        {
            get { return y; }
            private set
            {
                y = value;
                OnPropertyChanged();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
