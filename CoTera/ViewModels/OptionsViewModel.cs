using CommunityToolkit.Maui.Views;
using CoTera.Systems;
using Octokit;
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


        public List<YearOfStudies> YearsOfStudies
        {
            get => _yearsOfStudies;
            set
            {
                _yearsOfStudies = value;
                OnPropertyChanged(nameof(YearsOfStudies));
            }
        }
        List<YearOfStudies> _yearsOfStudies;

        public int SelectedYearOfStudiesIndex
        {
            get => _selectedYearOfStudiesIndex;
            set
            {
                _selectedYearOfStudiesIndex = value;
                OnPropertyChanged(nameof(SelectedYearOfStudiesIndex));
            }
        }
        int _selectedYearOfStudiesIndex;



        public OptionsViewModel()
        {
            _collages = new();
            _majors = new();
            _yearsOfStudies = new();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void SaveDataToLoader()
        {
            //DataLoaderSystem.SavedSelectedYear = Collages[SelectedCollageIndex];
            //DataLoaderSystem.SavedSelectedLab = Majors[SelectedMajorIndex];

            //DataLoaderSystem.GetSelectedOptionsContent();
        }
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
