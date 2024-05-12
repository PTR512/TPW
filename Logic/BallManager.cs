using Data.Abstract;
using System.Diagnostics;
using System.Runtime.Intrinsics;
namespace Logic;
internal class BallManager : LogicAPI
{
    private DataAPI Data;
    private bool isRunning = false;
    private List<IBall> Balls;
    private Object _locker = new Object();

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
                float mass = Data.getBallMass();
                IBall ball = IBall.CreateInstance(x, y, radius, xSpeed, ySpeed, false, mass);
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
        lock (_locker)
        {
            IBall Ball = (IBall)sender;
            (float x, float y) = Ball.getPosition();
            (float xSpeed, float ySpeed) = Ball.getSpeed();

            if (!WithinBoundariesOnAxis(x+xSpeed, Data.GetBallRadius(), Data.GetTableWidth()))
            {
                xSpeed = -xSpeed;
            }
            if (!WithinBoundariesOnAxis(y+ySpeed, Data.GetBallRadius(), Data.GetTableHeight()))
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
                    return;
                }
            }
            Ball.ChangeSpeed(xSpeed, ySpeed);
        }
    }
    private void ElasticCollision(IBall ball1, IBall ball2)
    {
        
            //System.Diagnostics.Debug.WriteLine("Collision with another ball");
            (float x1, float y1) = ball1.getPosition();
            (float xSpeed1, float ySpeed1) = ball1.getSpeed();
            (float x2, float y2) = ball2.getPosition();
            (float xSpeed2, float ySpeed2) = ball2.getSpeed();
            float m1 = ball1.getMass();
            float m2 = ball2.getMass();
            (float x, float y) newSpeedBall1, newSpeedBall2,
                               v1_v2 = (xSpeed1 - xSpeed2, ySpeed1 - ySpeed2),
                               v2_v1 = (xSpeed2 - xSpeed1, ySpeed2 - ySpeed1),
                               x1_x2 = (x1 - x2, y1 - y2),
                               x2_x1 = (x2 - x1, y2 - y1);
            float dist = EuclideanDistance(x1, y1, x2, y2);
            float sqDist = dist * dist;
            float coef1, coef2;
            coef1 = (v1_v2.x * x1_x2.x + v1_v2.y * x1_x2.y) / sqDist;
            coef2 = (v2_v1.x * x2_x1.x + v2_v1.y * x2_x1.y) / sqDist;
            float m1_m2 = (2*m2)/(m1+ m2);
            float m2_m1 = (2*m1)/(m1+ m2);
            newSpeedBall1 = (xSpeed1 - m1_m2 * x1_x2.x * coef1, ySpeed1 - m1_m2 * x1_x2.y * coef1);
            newSpeedBall2 = (xSpeed2 - m2_m1 * x2_x1.x * coef2, ySpeed2 - m2_m1 * x2_x1.y * coef2);
            ball1.ChangeSpeed(newSpeedBall1.x, newSpeedBall1.y);
            ball2.ChangeSpeed(newSpeedBall2.x, newSpeedBall2.y);
        


    }
    private bool IsColliding(IBall ball1, IBall ball2)
    {
        
        float radius = Data.GetBallRadius();
        (float x1, float y1) = ball1.getPosition();
        (float x1Speed, float y1Speed) = ball1.getSpeed();
        
        (float x2, float y2) = ball2.getPosition();
        (float x2Speed, float y2Speed) = ball2.getSpeed();

        if (EuclideanDistance(x1+x1Speed, y1+y1Speed, x2+x2Speed, y2+y2Speed) <= 2 * radius)
        {
            return true;
        }
        return false;
    }
    private float EuclideanDistance(float x1, float y1, float x2, float y2)
    {
        return (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

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
    private bool isOverlapping(float x1, float y1, float radius)
    {
        foreach (IBall Ball in Balls)
        {
            (float x2, float y2) = Ball.getPosition();
            if (EuclideanDistance(x1, y1, x2, y2) <= radius * 2)
            {
                return true;
            }
        }
        return false;
    }
    // generate random ball position within the boundaries of the table (radius <= x < TableWidth - radius) and (radius <= y < TableHeight - radius)
    private (float x, float y) GenerateRandomBallPlacement()
    {
        
        Random random = new();
        float radius = Data.GetBallRadius();
        // check if ball doesnt overlap with another ball
        float x, y;
        do
        {
            x = (float)random.NextDouble() * (Data.GetTableWidth() - 2 * radius) + radius;
            y = (float)random.NextDouble() * (Data.GetTableHeight() - 2 * radius) + radius;
        } while (isOverlapping(x, y, radius));
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

