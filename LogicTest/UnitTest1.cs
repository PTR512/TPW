using Data;
using Logic;
using Moq;
using System.ComponentModel;

namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void BallManagerTest()
        {
            Mock<IBall> BallSim = new Mock<IBall>();
            BallSim.Setup(b => b.StopBall());

            Mock<DataAPI> DataAPISim = new Mock<DataAPI>();
            DataAPISim.Setup(b => b.GetTableWidth()).Returns(500);
            DataAPISim.Setup(b => b.GetTableHeight()).Returns(700);
            DataAPISim.Setup(b => b.getMaxSpeed()).Returns(10);

            
            LogicAPI logicAPI = LogicAPI.CreateInstance(DataAPISim.Object);
            logicAPI.CreateBalls(2);

            Assert.IsTrue(logicAPI.GetBalls().Count == 2);
            logicAPI.RunSimulation();
            logicAPI.StopSimulation();

        }
    }
}