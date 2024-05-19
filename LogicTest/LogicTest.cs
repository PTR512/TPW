using Logic.Abstract;
using Moq;
using System.Numerics;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {

        [TestMethod]
        public void BallManagerTest()
        {
            Mock<Data.Abstract.IBall> BallSim = new Mock<Data.Abstract.IBall>();
            BallSim.Setup(b => b.StopBall());
            BallSim.Setup(b => b.LetBallMove());
            BallSim.Setup(b => b.getPosition()).Returns(new Vector2(1,1));
            BallSim.Setup(b => b.getSpeed()).Returns(new Vector2(1, 1));

            Mock<Data.Abstract.DataAPI> DataAPISim = new Mock<Data.Abstract.DataAPI>();
            DataAPISim.Setup(d => d.GetTableWidth()).Returns(500);
            DataAPISim.Setup(d => d.GetTableHeight()).Returns(700);
            DataAPISim.Setup(d => d.getMaxSpeed()).Returns(10);

            List<Data.Abstract.IBall> Balls = [BallSim.Object];
            Assert.IsNotNull(Balls);

            LogicAPI logicAPI = LogicAPI.CreateInstance(DataAPISim.Object, Balls);
            Assert.IsNotNull(logicAPI);
            Assert.IsTrue(logicAPI.GetBalls().Count == 1);

            logicAPI.RunSimulation();
            logicAPI.RunSimulation(); // the second RunSimulation method should do nothing
            BallSim.Verify(b => b.LetBallMove(), Times.Once);

            logicAPI.StopSimulation();
            BallSim.Verify(b => b.StopBall(), Times.Once);
            logicAPI.RunSimulation();
            BallSim.Verify(b => b.LetBallMove(), Times.Exactly(2));
        }
    }
}