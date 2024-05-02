using Data.Abstract;
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
    // create balls with random position and speed
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
    // check if the ball collides with a side of the table
    private void CheckCollisions(object? sender, EventArgs e)
    {

        IBall Ball = (IBall)sender;
        (float x, float y) = Ball.getPosition();
        (float xSpeed, float ySpeed) = Ball.getSpeed();
        if (!WithinBoundariesOnAxis(x, Data.GetBallRadius(), Data.GetTableWidth()))
        {
            xSpeed = -xSpeed;
        }
        if (!WithinBoundariesOnAxis(y, Data.GetBallRadius(), Data.GetTableHeight()))
        {
            ySpeed = -ySpeed;
        }
        // checking collisions with balls on the table
        foreach (IBall OtherBall in Balls)
        {
            if (OtherBall != Ball && IsColliding(Ball, OtherBall))
            {
                
                //function for calculating new velocity
                ElasticCollision(Ball, OtherBall);
            }
        }
        Ball.ChangeSpeed(xSpeed, ySpeed);
    }
    private void ElasticCollision(IBall ball1, IBall ball2)
    {
        (float xSpeed1, float ySpeed1) = ball1.getSpeed();
        (float xSpeed2, float ySpeed2) = ball2.getSpeed();

        throw new NotImplementedException();
    }
    private bool IsColliding(IBall ball1, IBall ball2)
    {
        float radius = Data.GetBallRadius();
        if (EuclideanDistance(ball1, ball2) <= 2 * radius)
        {
            return true;
        }
        return false;
    }
    private float EuclideanDistance(IBall ball1, IBall ball2)
    {
        (float x1, float y1) = ball1.getPosition();
        (float x2, float y2) = ball2.getPosition();
        return (float)Math.Sqrt(x1 * x1 + y1 * y2);

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
    // return a list of wrapped balls stripped from the ability to change the state of the original balls
    public override List<IBallPosition> GetBalls()
    {
        List<IBallPosition> ballPositions = new List<IBallPosition>();
        foreach (IBall Ball in Balls)
        {
            ballPositions.Add(IBallPosition.CreateInstance(Ball));
        }
        return ballPositions;
    }
    // generate random ball position within the boundaries of the table (radius <= x < TableWidth - radius) and (radius <= y < TableHeight - radius)
    private (float x, float y) GenerateRandomBallPlacement()
    {
        Random random = new();
        float radius = Data.GetBallRadius();
        float x = (float)random.NextDouble() * (Data.GetTableWidth() - 2 * radius) + radius;
        float y = (float)random.NextDouble() * (Data.GetTableHeight() - 2 * radius) + radius;
        return (x, y);
    }
    // generate random ball speed (-maxSpeed <= (xSpeed, ySpeed) < maxSpeed)
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

