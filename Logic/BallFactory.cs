using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class BallFactory
    {
        public static IBall CreateBall()
        {
            return IBall.CreateInstance(0, 0, 30, 0, 0, false);
        }
        public static List<IBall> CreateListOfBalls(int Amount)
        {
            List<IBall> list = new List<IBall>();
            for (int i = 0; i < Amount; i++)
            {
                list.Add(CreateBall());
            }
            return list;
        }
    }
}
