using Logic;

namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BallManagerTest()
        {
            LogicAPI logicAPI = LogicAPI.CreateInstance();
            
            Assert.IsTrue(logicAPI.Balls.Count == 2);
            logicAPI.RunSimulation();
            logicAPI.StopSimulation();

        }
    }
}