using Logic;

namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.Write("Test");
            LogicAPI logicAPI = LogicAPI.CreateInstance();
            logicAPI.CreateBalls(1);
            logicAPI.StopSimulation();
        }
    }
}