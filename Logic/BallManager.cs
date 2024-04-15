using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Logic;
internal class BallManager : LogicAPI
    {
    private DataAPI Data = DataAPI.CreateInstance();
    private bool isRunning = false;
    public BallManager()
    {
        Balls = [];

    }
    public BallManager(List<IBall> Balls)
    {
        this.Balls = Balls;
    }
    public override void CreateBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float radius = Data.GetBallRadius();
            (float x, float y) = GenerateRandomBallPlacement();
            IBall ball = IBall.CreateInstance(x, y, radius, 0,0,false);
            Balls.Add(ball);
            ball.CollisionEvent += CheckCollisions;
        }
        
        
    }
    
    
    
    public override void RunSimulation()
    {
        if (!isRunning)
        {
            foreach (IBall ball in Balls)
            {
                (float xSpeed, float ySpeed) = GenerateRandomBallSpeed();
                ball.ChangeSpeed(xSpeed, ySpeed);
                ball.LetBallMove();
                
            }
            isRunning = true;
        }
        

    }

    private void CheckCollisions(object? sender, EventArgs e)
    {

        IBall ball = (IBall) sender;
        (float x, float y) = ball.getPosition();
        (float xSpeed, float ySpeed) = ball.getSpeed();
        if (!WithinBoundariesOnAxis(x, Data.GetBallRadius(), Data.GetTableWidth()))
        {
            xSpeed = -xSpeed;
        }
        if (!WithinBoundariesOnAxis(y, Data.GetBallRadius(), Data.GetTableHeight()))
        {
            ySpeed = -ySpeed;
        }
        ball.ChangeSpeed(xSpeed, ySpeed);
    }

    public override void StopSimulation()
    {
        if (isRunning)
        {
            foreach (IBall ball in Balls)
            {
                ball.StopBall();
            }
            isRunning = false;
        }
        
    }
    public override List<IBall> Balls { get; }

    private (float x, float y) GenerateRandomBallPlacement()
    {
        Random random = new();
        float radius = Data.GetBallRadius();
        float x = (float) random.NextDouble() * (Data.GetTableWidth() - 2 * radius) + radius;
        float y = (float) random.NextDouble() * (Data.GetTableHeight() - 2 * radius) + radius;
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

