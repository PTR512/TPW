using Data.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
namespace Data
{
    internal record BallInfoRecord(DateTime timestamp, int id, float x, float y, float xSpeed, float ySpeed);
    static internal class Logger
    {
        static BlockingCollection<BallInfoRecord> blockingCollection = new BlockingCollection<BallInfoRecord>(15);
        static StreamWriter file = new StreamWriter($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}/logs.json", append: true);

    static public void logBallInfo(int id, Vector4 ballInfo, DateTime timestamp)
        {
            BallInfoRecord info = new(timestamp, id, ballInfo[0], ballInfo[1], ballInfo[2], ballInfo[3]);
            blockingCollection.TryAdd(info);

        }
        static public async void saveLogToFile()
        {
       
                try
                {
                    foreach (var item in blockingCollection.GetConsumingEnumerable())
                    {
                    string ballInfoString = JsonSerializer.Serialize(item);
                    file.Write(ballInfoString);
                    }
                }
                catch (Exception e)
                {

                }
            
            
            file.Close();

        }
    }
}
