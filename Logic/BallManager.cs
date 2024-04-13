using Data;
namespace Logic;
internal class BallManager : LogicAPI
    {
    // TODO: make observers thread-safe, 'lock' instruction i guess
    private DataAPI Data = DataAPI.CreateInstance();
    private ISet<IObserver<List<IBall>>> observers;
    private bool isRunning = false;
    public BallManager()
    {
        Balls = [];
        observers = new HashSet<IObserver<List<IBall>>>();
        Console.Write("dziala");

    }
    public override void CreateBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float radius = Data.GetBallRadius();
            (float x, float y) = GenerateRandomBallPlacement();
            Balls.Add(IBall.CreateInstance(x, y, radius, 0,0, false));
        }
        
        
    }
    private void MoveBalls()
    {
        foreach(IBall ball in Balls)
        {
            ball.LetBallMove();
        }
    }
    public void UpdateObservers()
    {
        foreach(var observer in observers)
        {
            if (Balls != null)
            {
                observer.OnNext(Balls);
            }
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

    public override IDisposable Subscribe(IObserver<List<IBall>> observer)
    {
        observers.Add(observer);
        return new Unsubscriber(observers, observer);
    }

    private class Unsubscriber(ISet<IObserver<List<IBall>>> observers, IObserver<List<IBall>> observer) : IDisposable
    {
        private readonly ISet<IObserver<List<IBall>>> observers = observers;
        private readonly  IObserver<List<IBall>> observer = observer;

        public void Dispose()
        {
            observers?.Remove(observer);
        }  
    }
}

