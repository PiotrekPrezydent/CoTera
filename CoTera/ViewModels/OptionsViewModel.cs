using System.ComponentModel;
using URAPI;

namespace CoTera.ViewModels
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        public List<Collage> Collages
        {
            get => _collages;
            set
            {
                _collages = value;
                OnPropertyChanged(nameof(Collages));
            }
        }
        List<Collage> _collages;

        public int SelectedCollageIndex
        {
            get => _selectedCollageIndex;
            set
            {
                _selectedCollageIndex = value;
                OnPropertyChanged(nameof(SelectedCollageIndex));
            }
        }
        int _selectedCollageIndex;

        public List<Major> Majors
        {
            get => _majors;
            set
            {
                _majors = value;
                OnPropertyChanged(nameof(Majors));
            }
        }
        List<Major> _majors;

        public int SelectedMajorIndex
        {
            get => _selectedMajorIndex;
            set
            {
                _selectedMajorIndex = value;
                OnPropertyChanged(nameof(SelectedMajorIndex));
            }
        }
        int _selectedMajorIndex;


        public List<Schedule> Schedules
        {
            get => _schedules;
            set
            {
                _schedules = value;
                OnPropertyChanged(nameof(Schedules));
            }
        }
        List<Schedule> _schedules;

        public int SelectedScheduleIndex
        {
            get => _selectedScheduleIndex;
            set
            {
                _selectedScheduleIndex = value;
                OnPropertyChanged(nameof(SelectedScheduleIndex));
            }
        }
        int _selectedScheduleIndex;

        public OptionsViewModel()
        {
            _collages = new();
            _majors = new();
            _schedules = new();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
