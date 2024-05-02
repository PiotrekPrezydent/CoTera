using CoTera.Systems;
using System.ComponentModel;

namespace CoTera.ViewModels
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        public List<string> LoadedYears
        {
            get => _loadedYears;
            set
            {
                _loadedYears = value;
                OnPropertyChanged(nameof(LoadedYears));
            }
        }
        List<string> _loadedYears;

        public int SelectedYearIndex
        {
            get => _selectedYearIndex;
            set
            {
                _selectedYearIndex = value;
                OnPropertyChanged(nameof(SelectedYearIndex));
            }
        }
        int _selectedYearIndex;

        public List<string> LoadedLabs
        {
            get => _loadedLabs;
            set
            {
                _loadedLabs = value;
                OnPropertyChanged(nameof(LoadedLabs));
            }
        }
        List<string> _loadedLabs;

        public int SelectedLabIndex
        {
            get => _selectedLabIndex;
            set
            {
                _selectedLabIndex = value;
                OnPropertyChanged(nameof(SelectedLabIndex));
            }
        }
        int _selectedLabIndex;


        public OptionsViewModel()
        {
            LoadedYears = new List<string>();
            LoadedLabs = new List<string>();

            LoadedYears.Add("-");
            LoadedLabs.Add("-");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void SaveDataToLoader()
        {
            DataLoaderSystem.SavedSelectedYearIndex = SelectedYearIndex;
            DataLoaderSystem.SelectedLabIndex = SelectedLabIndex;

            MainPage.DEBUG = DataLoaderSystem.SavedSelectedYearIndex + " /// " + DataLoaderSystem.SelectedLabIndex;
        }
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
