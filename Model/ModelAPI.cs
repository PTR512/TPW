
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAPI
    {
        public abstract void CreateBalls();
        public abstract void Start();
        public abstract void Stop();
        public abstract ObservableCollection<object> GetBalls();
        public abstract int BallAmount { get; set; }

        public static ModelAPI CreateInstance()
        {
            return new Model();
        }
    }
}
