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

        //Td: get week type from this site this must be hardcodded
        //https://www.ur.edu.pl/files/user_directory/899/organizacja%20roku%20akademickiego/Zarz%C4%85dzenie%20nr%2077_2023%20ws.%20organizacji%20roku%20akademickiego%202023-2024.pdf
        internal DateTime ChosenDate
        {
            get => _chosenDate;
            set
            {
                _chosenDate = value;
                NameOfDay = _chosenDate.DayOfWeek.ToString() + "\n" + GetWeekSpanAsString(_chosenDate);
                ShowClassesForCurrentDay();
            }
        }
        DateTime _chosenDate;

        public event PropertyChangedEventHandler? PropertyChanged;

        internal DayView[] LoadedDays = new DayView[7];

        public MainViewModel()
        {
            for (int i = 0; i < 7; i++)
            {
                ClassView[] classes = { new ClassView("UNKNOWN", "UNKNOWN") };
                LoadedDays[i] = new DayView((DayOfWeek)i, classes);
            }
            ChosenDate = DateTime.Today;
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
            MainPage.Classes.ItemsSource = day.Classes.Select(e => e.Name + "\n" + e.TimeSpan);
        }

        string GetWeekSpanAsString(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
                date = date.AddDays(-1);

            string returnedValue = date.ToString().Substring(0, 10) + " - " + date.AddDays(6).ToString().Substring(0, 10);
            return returnedValue;
        }

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
