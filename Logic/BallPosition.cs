using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class BallPosition : IBallPosition
    {
        private float x, y;

        private IBall Ball;
        public override event PropertyChangedEventHandler? PropertyChanged;

        public BallPosition(IBall Ball)
        {
            this.Ball = Ball;
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
        public void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
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

    }
}
