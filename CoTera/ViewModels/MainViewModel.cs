using CoTera.Views;
using System.ComponentModel;

namespace CoTera.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public string NameOfDay 
        {
            get => _nameOfDay;
            set
            {
                _nameOfDay = value;
                OnPropertyChanged(nameof(NameOfDay));
            } 
        }
        string _nameOfDay;
        
        internal DayOfWeek ChosenDay
        {
            get => _chosenDay;
            set
            {
                if (value == _chosenDay)
                    throw new Exception("Overrided day with same value");

                if(value > _chosenDay)
                {
                    if (_chosenDay == DayOfWeek.Saturday)
                        value = DayOfWeek.Sunday;
                }
                else
                {
                    if (_chosenDay == DayOfWeek.Sunday)
                        value = DayOfWeek.Saturday;
                }

                _chosenDay = value;
                NameOfDay = _chosenDay.ToString();
                ShowClassesForCurrentDay();
            }
        }
        DayOfWeek _chosenDay;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DayView[] LoadedDays = new DayView[7];

        public MainViewModel()
        {
            for (int i = 0; i < 7; i++)
            {
                ClassView[] classes = { new ClassView("UNKNOWN", "UNKNOWN") };
                LoadedDays[i] = new DayView((DayOfWeek)i, classes);
            }
            ChosenDay = DateTime.Today.DayOfWeek;
        }

        internal void ShowClassesForCurrentDay()
        {
            int index = 0;
            if (ChosenDay == DayOfWeek.Sunday)
                index = 7;
            else
                index = (int)ChosenDay;

            index--;

            DayView day = LoadedDays[index];
            MainPage.Classes.ItemsSource = day.Classes.Select(e => e.Name + "\n" + e.TimeSpan);
        }

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
