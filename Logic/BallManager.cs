using Data;
namespace Logic;
internal class BallManager : LogicAPI
    {
    private DataAPI Data = DataAPI.CreateInstance();
    public override void CreateBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float radius = Data.GetBallRadius();
            (float x, float y) = GenerateRandomBallPlacement();
            Balls.Add(IBall.CreateInstance(x, y, radius, 0,0));
        }
        
        
    }

    public override void RunSimulation()
    {
        foreach (IBall ball in Balls)
        {
            (float xSpeed, float ySpeed) = GenerateRandomBallSpeed();
            ball.ChangeSpeed(xSpeed, ySpeed);
        }
    }

    public override void StopSimulation()
    {
        foreach (IBall ball in Balls)
        {
            ball.ChangeSpeed(0, 0);
        }
    }
    private List<IBall> Balls { get; }

    public BallManager()
    {
        Balls = [];

    }
    private (float x, float y) GenerateRandomBallPlacement()
    {
        Random random = new();
        float radius = Data.GetBallRadius();
        float x = (float) random.NextDouble() * (Data.GetTableWidth() - radius) + radius;
        float y = (float) random.NextDouble() * (Data.GetTableHeight() - radius) + radius;
        return (x, y);
    }
    private (float xSpeed, float ySpeed) GenerateRandomBallSpeed()
    {
        Random random = new();
        float maxSpeed = Data.getMaxSpeed();
        float xSpeed = (float)random.NextDouble() * maxSpeed;
        float ySpeed = (float)random.NextDouble() * maxSpeed;
        return (xSpeed, ySpeed);
    }
    private static bool WithinBoundariesOnAxis(float pos, float radius, float boundary)
    {
        return 0 <= (pos - radius) && (pos + radius) <= boundary;
    }
}

