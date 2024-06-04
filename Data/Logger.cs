using Data.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Data
{
    internal class Logger
    {
        static BlockingCollection<FormattableString> blockingCollection = new BlockingCollection<FormattableString>(15);

        static public void logBallInfo(IBall ball)
        {
            Vector4 ballInfo = ball.getPositionAndSpeed();
            int id = 0;
            FormattableString ballInfoString = $"{{\"Time\": \"{DateTime.Now}\", \"id\":\"{id}\", \"x\": \"{ballInfo[0]}\", \"y\": \"{ballInfo[1]}\", \"xSpeed\": \"{ballInfo[2]}\", \"ySpeed\": \"{ballInfo[3]}\" }}"; // chyba jakies id tych kulek by sie przydalo
            blockingCollection.TryAdd(ballInfoString);

        }
        static public void saveLogToFile()
        {
            try
            {
                using StreamWriter file = new StreamWriter("logs.json", append: true);
                foreach (var item in blockingCollection.GetConsumingEnumerable())
                {
                        file.Write(item);
                }
                file.Close();
            }
            catch(Exception e)
            {

            }
        }
    }
}
