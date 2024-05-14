using Data.Abstract;
namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallTest()
        {
            IBall ball = IBall.CreateInstance(0, 0, 1, 1, 1, false, 1);
            Assert.IsNotNull(ball);
            Assert.IsTrue((1, 1) == ball.getSpeed());
            Assert.IsTrue((0, 0) == ball.getPosition());
            ball.LetBallMove();
            ball.ChangeSpeed(1, 2);
            Assert.IsTrue((1, 2) == ball.getSpeed());
        }
        [TestMethod]
        public void TableTest()
        {
            DataAPI data = DataAPI.CreateInstance();
            Assert.IsTrue(data.GetTableWidth() == 500);
            Assert.IsTrue(data.GetTableHeight() == 700);
            Assert.IsTrue(data.getMaxSpeed() == 3);
            Assert.IsTrue(data.GetBallRadius() == 30);
            Assert.IsTrue(data.getBallMass() == 10);

        }
    }
}