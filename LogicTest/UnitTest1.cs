using Logic;

namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("Test");
            LogicAPI logicAPI = LogicAPI.CreateInstance();
            logicAPI.CreateBalls(2);

            logicAPI.RunSimulation();
        }
    }
}