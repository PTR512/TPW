﻿namespace Data
{
    internal class Data : Abstract.DataAPI

    {
        public Data() { }

        private readonly float width = 500;
        private readonly float height = 700;
        private readonly float ballRadius = 30;
        private readonly float maxSpeed = 3;
        private readonly float ballMass = 10;

        public override float GetBallRadius()
        {
            return ballRadius;
        }

        public override float getMaxSpeed()
        {
            return maxSpeed;
        }

        public override float getBallMass()
        {
            return ballMass;
        }

        public override float GetTableHeight()
        {
            return height;
        }

        public override float GetTableWidth()
        {
            return width;
        }
    }
}
