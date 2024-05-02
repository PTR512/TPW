namespace Data
{
    internal class Data : Abstract.DataAPI

    {
        private readonly float width = 500;
        private readonly float height = 700;
        private readonly float ballRadius = 30;
        private readonly float maxSpeed = 5;

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
