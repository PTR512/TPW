using System.Collections.ObjectModel;

namespace ViewModel
{
    public abstract class ViewModelAPI
    {
        public abstract ObservableCollection<object> Balls { get; set; }
    }
}
