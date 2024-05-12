namespace Data.Abstract
{
    public abstract class DataAPI
    {
        public abstract float GetTableWidth();
        public abstract float GetTableHeight();
        public abstract float GetBallRadius();
        public abstract float getMaxSpeed();
        public abstract float getBallMass();

        public static DataAPI CreateInstance()
        {
            return new Data();
        }
    }
}
