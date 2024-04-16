using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Logic;
internal class BallManager : LogicAPI
    {
    private DataAPI Data;
    private bool isRunning = false;
    private List<IBall> Balls;

    public BallManager(DataAPI Data, List<IBall> Balls)
    {
        this.Balls = Balls;
        this.Data = Data;
    }
    
    public override void CreateBalls(int amount)
    {
        if (Balls.Count == 0)
        {
            for (int i = 0; i < amount; i++)
            {
                float radius = Data.GetBallRadius();
                (float x, float y) = GenerateRandomBallPlacement();
                (float xSpeed, float ySpeed) = GenerateRandomBallSpeed();
                IBall ball = IBall.CreateInstance(x, y, radius, xSpeed, ySpeed, false);
                Balls.Add(ball);
                ball.ChangedPosition += CheckCollisions;
            }
        }
        
        
        
    }
    
    
    
    public override void RunSimulation()
    {
        if (!isRunning)
        {
            foreach (IBall ball in Balls)
            {
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
    public override List<IBallPosition> GetBalls()
    {
        List<IBallPosition> ballPositions = new List<IBallPosition>();
        foreach (IBall Ball in Balls)
        {
            ballPositions.Add(IBallPosition.CreateInstance(Ball));
        }
        return ballPositions;
    }
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
        float xSpeed = ((float)random.NextDouble() * 2 * maxSpeed) - maxSpeed;
        float ySpeed = ((float)random.NextDouble() * 2 * maxSpeed) - maxSpeed;
        return (xSpeed, ySpeed);
    }
    private static bool WithinBoundariesOnAxis(float pos, float radius, float boundary)
    {
        return 0 <= (pos - radius) && (pos + radius) <= boundary;
    }

    
}

