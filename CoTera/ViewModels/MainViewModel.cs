using CoTera.Systems;
using CoTera.Views;
using System.ComponentModel;
using System.Data;
using URAPI;

namespace CoTera.ViewModels
{
    //td getweekspanasstring should be called only when changing week, the same goes with getweektype
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

        internal DateTime ChosenDate
        {
            get => _chosenDate;
            set
            {
                _chosenDate = value;
                NameOfDay = _chosenDate.DayOfWeek.ToString() + "\n" + GetWeekSpanAsString(_chosenDate) + "\nWeek: "+GetWeekType(_chosenDate);
                ShowClassesForCurrentDay();
            }
        }
        DateTime _chosenDate;

        public List<string> CurrentDayClasses
        {
            get => _currentDayClasses;
            set
            {
                _currentDayClasses = value;
                OnPropertyChanged(nameof(CurrentDayClasses));
            }
        }
        List<string> _currentDayClasses;

        public event PropertyChangedEventHandler? PropertyChanged;

        internal DayView[] LoadedDays = new DayView[7];

        public MainViewModel()
        {
            CurrentDayClasses = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                ClassView[] classes = { new ClassView("UNKNOWN", "UNKNOWN") };
                LoadedDays[i] = new DayView((DayOfWeek)i, classes);
                CurrentDayClasses.Add("UNKNOWN");
            };
            ChosenDate = DateTime.Today;
        }

        public void Show()
        {

        }

        internal void ShowClassesForCurrentDay()
        {
            int index = 0;
            if (ChosenDate.DayOfWeek == DayOfWeek.Sunday)
                index = 7;
            else
                index = (int)ChosenDate.DayOfWeek;

            index--;

            DayView day = LoadedDays[index];

            CurrentDayClasses = day.Classes.Where(e => e.Week == "A+B" || e.Week == GetWeekType(ChosenDate) || e.Week=="" ).Select(e => e.GetClassInfo()).ToList();
        }

        string GetWeekSpanAsString(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
                date = date.AddDays(-1);

            string returnedValue = date.ToString().Substring(0, 10) + " - " + date.AddDays(6).ToString().Substring(0, 10);
            return returnedValue;
        }

        string GetWeekType(DateTime date)
        {
            if (DataLoaderSystem.LoadedWeeksTypeA == null)
                return "?";

            if (DataLoaderSystem.LoadedWeeksTypeA.Any(e => GetWeekSpanAsString(date).Contains(e)))
                return "A";

            return "B";
        }

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}