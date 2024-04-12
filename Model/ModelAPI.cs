

using Data;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAPI
    {
        public abstract void CreateBalls(int amount);
        public abstract void Start();
        public abstract void Stop();
        public abstract ObservableCollection<object> GetBalls();

        public static ModelAPI CreateInstance()
        {
            return new Model();
        }
    }
}
