using Data.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Data
{
    internal record BallInfoRecord(DateTime timestamp, int id, float x, float y, float xSpeed, float ySpeed);

    internal class Logger : IDisposable
    {
        private BlockingCollection<BallInfoRecord> blockingCollection;
        private bool isDisposed = false;
        private static Logger logger = null;
        private static Thread _worker = null;

        private Logger()
        {
            blockingCollection = new BlockingCollection<BallInfoRecord>(15);
        }

        public static Logger getLogger()
        {
            if (logger == null)
            {
                logger = new Logger();
                _worker = new Thread(new ThreadStart(logger.saveLogToFile));
                _worker.Start();
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

                using (StreamWriter file = new StreamWriter($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}/logs.json", append: true, Encoding.UTF8))
                { 
                    foreach (var item in blockingCollection.GetConsumingEnumerable())
                    {
                        string ballInfoString = JsonSerializer.Serialize(item);
                        await file.WriteLineAsync(ballInfoString);
                    }
                }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    blockingCollection.CompleteAdding();
                    if (_worker != null)
                    {
                        _worker.Join();
                        _worker = null;
                    }
                    blockingCollection.Dispose();
                }
                isDisposed = true;
            }
        }

        ~Logger()
        {
            Dispose(false);
        }
    }
}
