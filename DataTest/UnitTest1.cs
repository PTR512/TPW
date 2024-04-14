using Data;
namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IBall.CreateInstance(0, 0, 1, 1, 1, true);

        }
    }
}