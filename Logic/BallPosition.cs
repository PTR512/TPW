using Data.Abstract;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class BallPosition : Abstract.IBallPosition
    {
        Vector2 position;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public BallPosition(IBall Ball)
        {
            position = Ball.getPosition();
            X = position.X;
            Y = position.Y;
            Ball.ChangedPosition += OnChagedPosition;
        }

        private void OnChagedPosition(object? sender, EventArgs e)
        {

            IBall Ball = (IBall)sender;
            position = Ball.getPosition();
            X = position.X;
            Y = position.Y;
        }

        public float X
        {
            get { return position[0]; }
            private set
            {
                position.X = value;
                OnPropertyChanged();
            }
        }
        public float Y
        {
            get { return position[1]; }
            private set
            {
                position.Y = value;
                OnPropertyChanged();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
