using Data.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
namespace Data
{
    internal record BallInfoRecord(DateTime timestamp, int id, float x, float y, float xSpeed, float ySpeed);
    internal class Logger : IDisposable
    {
        private BlockingCollection<BallInfoRecord> blockingCollection;
        private StreamWriter file;
        private bool isDisposed = false;
        static private Logger logger = null;
        private Logger()
        {
            blockingCollection = new BlockingCollection<BallInfoRecord>(15);
            file = new StreamWriter($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}/logs.json", append: true, Encoding.UTF8);
        }
        

        static public Logger getLogger()
        {
            if (logger == null)
            {
                logger = new Logger();
                new Thread(new ThreadStart(logger.saveLogToFile)).Start();
                

            }
            return logger;
        }
        public void logBallInfo(int id, Vector4 ballInfo, DateTime timestamp)
        {
            BallInfoRecord info = new(timestamp, id, ballInfo[0], ballInfo[1], ballInfo[2], ballInfo[3]);
            blockingCollection.TryAdd(info);

        }
        private async void saveLogToFile()
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
        void IDisposable.Dispose()
        {
            if (!isDisposed)
            {
                blockingCollection.CompleteAdding();
                // tutaj thread.join() i thread.dispose
                blockingCollection.Dispose();
                file.Flush ();  // chyba
                file.Dispose();
                isDisposed = true;
            }
        }
    }
}
