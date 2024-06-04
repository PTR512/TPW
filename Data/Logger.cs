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
    static internal class Logger
    {
        static BlockingCollection<FormattableString> blockingCollection = new BlockingCollection<FormattableString>(15);
        static StreamWriter file = new StreamWriter($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}/logs.json", append: true);

    static public void logBallInfo(int id, Vector4 ballInfo)
        {
            FormattableString ballInfoString = $"{{\"Time\": \"{DateTime.Now}\", \"id\":\"{id}\", \"x\": \"{ballInfo[0]}\", \"y\": \"{ballInfo[1]}\", \"xSpeed\": \"{ballInfo[2]}\", \"ySpeed\": \"{ballInfo[3]}\" }} \n";
            blockingCollection.TryAdd(ballInfoString);

        }
        static public async void saveLogToFile()
        {
       
                try
                {
                    foreach (var item in blockingCollection.GetConsumingEnumerable())
                    {
                        file.Write(item);
                    await Task.Delay(100);
                    }
                }
                catch (Exception e)
                {

                }
            
            
            file.Close();

        }
    }
}
