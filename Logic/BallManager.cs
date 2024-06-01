using Data.Abstract;
using System.Numerics;
namespace Logic;
internal class BallManager : Abstract.LogicAPI
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
                Vector2 position = GenerateRandomBallPlacement();
                Vector2 speed = GenerateRandomBallSpeed();
                float mass = Data.getBallMass();
                IBall ball = IBall.CreateInstance(position, radius, speed, false, mass);
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
    // check if the ball collides with a side of the table or another ball
    private void CheckCollisions(object? sender, EventArgs e)
    {
        IBall Ball = (IBall)sender;
        lock (_locker)
        {
            Vector4 positionAndSpeed1 = Ball.getPositionAndSpeed();
            float x = positionAndSpeed1[0];
            float y = positionAndSpeed1[1];
            float xSpeed = positionAndSpeed1[2];
            float ySpeed = positionAndSpeed1[3];
            if (CheckWallCollision(x + xSpeed, xSpeed, Data.GetBallRadius(), Data.GetTableWidth()))
            {
                xSpeed = -xSpeed;
            }
            if (CheckWallCollision(y + ySpeed, ySpeed, Data.GetBallRadius(), Data.GetTableHeight()))
            {
                ySpeed = -ySpeed;
            }
            Vector2 speed = new Vector2(xSpeed, ySpeed);
            Ball.ChangeSpeed(speed);
            // checking collisions with balls on the table
            foreach (IBall OtherBall in Balls)
            {
                Vector4 positionAndSpeed2 = OtherBall.getPositionAndSpeed();
                if (OtherBall != Ball && IsColliding(Ball, positionAndSpeed1, OtherBall, positionAndSpeed2))
                {
                    //function for calculating new velocity
                    ElasticCollision(Ball, positionAndSpeed1, OtherBall, positionAndSpeed2);
                    return;
                }
            }
            
        }
    }
    private void ElasticCollision(IBall ball1, Vector4 positionAndSpeed1, IBall ball2, Vector4 positionAndSpeed2)
    {

        float x1 = positionAndSpeed1[0];
        float y1 = positionAndSpeed1[1];
        float xSpeed1 = positionAndSpeed1[2];
        float ySpeed1 = positionAndSpeed1[3];
        
        float x2 = positionAndSpeed2[0];
        float y2 = positionAndSpeed2[1];
        float xSpeed2 = positionAndSpeed2[2];
        float ySpeed2 = positionAndSpeed2[3];
        float m1 = Data.getBallMass();
        float m2 = Data.getBallMass();
        Vector2 newSpeedBall1, newSpeedBall2;
        (float x, float y) v1_v2 = (xSpeed1 - xSpeed2, ySpeed1 - ySpeed2),
                           v2_v1 = (xSpeed2 - xSpeed1, ySpeed2 - ySpeed1),
                           x1_x2 = (x1 - x2, y1 - y2),
                           x2_x1 = (x2 - x1, y2 - y1);
        float dist = EuclideanDistance(x1, y1, x2, y2);
        float sqDist = dist * dist;
        float coef1, coef2;
        coef1 = (v1_v2.x * x1_x2.x + v1_v2.y * x1_x2.y) / sqDist;
        coef2 = (v2_v1.x * x2_x1.x + v2_v1.y * x2_x1.y) / sqDist;
        float m1_m2 = (2 * m2) / (m1 + m2);
        float m2_m1 = (2 * m1) / (m1 + m2);
        newSpeedBall1.X = (xSpeed1 - m1_m2 * x1_x2.x * coef1);
        newSpeedBall1.Y = (ySpeed1 - m1_m2 * x1_x2.y * coef1);
        newSpeedBall2.X = (xSpeed2 - m2_m1 * x2_x1.x * coef2);
        newSpeedBall2.Y = (ySpeed2 - m2_m1 * x2_x1.y * coef2);
        ball1.ChangeSpeed(newSpeedBall1);
        ball2.ChangeSpeed(newSpeedBall2);



    }
    private bool IsColliding(IBall ball1, Vector4 positionAndSpeed1, IBall ball2, Vector4 positionAndSpeed2)
    {

        float radius = Data.GetBallRadius();
        float x1 = positionAndSpeed1[0];
        float y1 = positionAndSpeed1[1];
        float x1Speed = positionAndSpeed1[2];
        float y1Speed = positionAndSpeed1[3];

        float x2 = positionAndSpeed2[0];
        float y2 = positionAndSpeed2[1];
        float x2Speed = positionAndSpeed2[2];
        float y2Speed = positionAndSpeed2[3];

        if (EuclideanDistance(x1 + x1Speed, y1 + y1Speed, x2 + x2Speed, y2 + y2Speed) <= 2 * radius)
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
    public override List<Abstract.IBallPosition> GetBalls()
    {
        List<Abstract.IBallPosition> ballPositions = new List<Abstract.IBallPosition>();
        foreach (IBall Ball in Balls)
        {
            ballPositions.Add(Abstract.IBallPosition.CreateInstance(Ball));
        }
        return ballPositions;
    }
    // function for checking if balls are overlapping during generating random placement
    private bool isOverlapping(float x1, float y1, float radius) 
    {
        foreach (IBall Ball in Balls)
        {
            Vector4 vector4 = Ball.getPositionAndSpeed();
            float x2 = vector4[0];
            float y2 = vector4[1];
            if (EuclideanDistance(x1, y1, x2, y2) <= radius * 2)
            {
                return true;
            }
        }
        return false;
    }
    // generate random ball position within the boundaries of the table (radius <= x < TableWidth - radius) and (radius <= y < TableHeight - radius)
    private Vector2 GenerateRandomBallPlacement()
    {

        Random random = new();
        float radius = Data.GetBallRadius();
        // check if ball doesnt overlap with another ball

        Vector2 position;
        do
        {
            position.X = (float)random.NextDouble() * (Data.GetTableWidth() - 2 * radius) + radius;
            position.Y = (float)random.NextDouble() * (Data.GetTableHeight() - 2 * radius) + radius;
        } while (isOverlapping(position.X, position.Y, radius));
        return position;
    }
    // generate random ball speed (-maxSpeed <= (xSpeed, ySpeed) < maxSpeed)
    private Vector2 GenerateRandomBallSpeed()
    {
        Random random = new();
        float maxSpeed = Data.getMaxSpeed();
        Vector2 speed;
        speed.X = ((float)random.NextDouble() * 2 * maxSpeed) - maxSpeed;
        speed.Y = ((float)random.NextDouble() * 2 * maxSpeed) - maxSpeed;
        return speed;
    }
    private static bool CheckWallCollision(float pos, float speed, float radius, float boundary)
    {
        if ((pos - radius) <= 0 && speed < 0) { return true; }
        if ((pos + radius) >= boundary && speed > 0) { return true; }
        return false;
    }


}

