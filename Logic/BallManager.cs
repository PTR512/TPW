namespace Logic;
internal class BallManager : LogicAPI
    {
    public override void CreateBalls(int amount)
    {
        throw new NotImplementedException();
    }

    public override void RunSimulation()
    {
        throw new NotImplementedException();
    }

    public override void StopSimulation()
    {
        throw new NotImplementedException();
    }
    private List<Ball> Balls { get; }

    public BallManager()
    {
        Balls = [];


    }
}

