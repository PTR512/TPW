﻿using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class Model : ModelAPI
    {
        private ObservableCollection<object> _balls = new ObservableCollection<object>();
        private LogicAPI logicAPI = LogicAPI.CreateInstance();
        private int _ballAmount = 0;
        public override int BallAmount { get => _ballAmount; set => _ballAmount = value; }

        public override void CreateBalls()
        {
            logicAPI.CreateBalls(_ballAmount);
            System.Diagnostics.Debug.WriteLine("BALLS: " + GetBalls().Count);
        }

        public override ObservableCollection<object> GetBalls()
        {
            foreach (object ball in logicAPI.Balls)
            {
                _balls.Add(ball);
            }
            return _balls;
        }

        public override void Start()
        {
            logicAPI.RunSimulation();
        }

        public override void Stop()
        {
            logicAPI.StopSimulation();
        }
    }
}
