using Data.Abstract;
using System.Numerics;
namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallTest()
        {
            Vector2 position = new Vector2(0, 0);
            Vector2 speed = new Vector2(1, 1);
            IBall ball = IBall.CreateInstance(position, 1, speed, false, 1);
            Assert.IsNotNull(ball);
            Assert.IsTrue(1 == ball.getSpeed().X);
            Assert.IsTrue(1 == ball.getSpeed().Y);
            Assert.IsTrue(0 == ball.getPosition().X);
            Assert.IsTrue(0 == ball.getPosition().Y);
            ball.LetBallMove();
            Vector2 newSpeed = new Vector2(2, 3);
            ball.ChangeSpeed(newSpeed);
            Assert.IsTrue(2 == ball.getSpeed().X);
            Assert.IsTrue(3 == ball.getSpeed().Y);
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