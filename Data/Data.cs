

using System.ComponentModel.DataAnnotations;

namespace Data
{
    internal class Data : DataAPI
        
    {
        private readonly float width = 500;
        private readonly float height = 700;
        private readonly float ballRadius = 10;
        private readonly float maxSpeed = 10;

        public override float GetBallRadius()
        {
            return ballRadius;
        }

        public override float getMaxSpeed()
        {
            return maxSpeed;
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
