using System.ComponentModel;

namespace CoTera.ViewModels
{
    //td getweekspanasstring should be called only when changing week, the same goes with getweektype
    internal class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;


        public MainViewModel()
        {

        }

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}