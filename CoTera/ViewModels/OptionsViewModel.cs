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
                SelectedCollageIndex = 0;
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
                Task.Run(async () =>
                {
                    Majors = await Collages[SelectedCollageIndex].GetMajors();
                }).Wait();
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
                SelectedMajorIndex = 0;
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
                Task.Run(async () =>
                {
                    YearsOfStudies = await Majors[SelectedMajorIndex].GetYearOfStudies();
                }).Wait();
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
                SelectedYearOfStudiesIndex = 0;
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
