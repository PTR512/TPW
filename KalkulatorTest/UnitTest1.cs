

namespace KalkulatorTest
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void TestAdd()
        {
            KalkulatorNamespace.Kalkulator kal = new KalkulatorNamespace.Kalkulator();
            int val1 = new Random().Next();
            int result = kal.add(val1, 0);

            Assert.AreEqual(val1, result);
        }
    }
}