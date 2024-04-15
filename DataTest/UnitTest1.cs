using Data;
namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BallTest()
        {
            IBall ball = IBall.CreateInstance(0, 0, 1, 1, 1, false);
            Assert.IsNotNull(ball);
            Assert.IsTrue((1, 1) == ball.getSpeed());
            Assert.IsTrue((0, 0) == ball.getPosition());
            ball.LetBallMove();
            ball.ChangeSpeed(1,2);
            Assert.IsTrue((1, 2) == ball.getSpeed());
        }
        public void TableTest()
        {
            DataAPI data = DataAPI.CreateInstance();
            Assert.IsTrue(data.GetTableWidth() == 700);
            Assert.IsTrue(data.GetTableHeight() == 500);
            Assert.IsTrue(data.getMaxSpeed() == 5);
            Assert.IsTrue(data.GetBallRadius() == 30);
        }
    }
}