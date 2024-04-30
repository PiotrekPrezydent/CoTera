using CoTera.Systems;

namespace CoTera.ViewModels
{
    internal class OptionsViewModel
    {
        public string SelectedYear;

        public string SelectedLab;

        public OptionsViewModel()
        {
            //Sample values for first boot
            SelectedYear = "Test1";
            SelectedLab = "Test6";

            if(DataLoaderSystem.SelectedYear == null)
                DataLoaderSystem.SelectedYear = SelectedYear;
            if(DataLoaderSystem.SelectedLab == null)
                DataLoaderSystem.SelectedLab = SelectedLab;
        }

        internal void SaveDataToLoader()
        {
            DataLoaderSystem.SelectedYear = SelectedYear;
            DataLoaderSystem.SelectedLab = SelectedLab;

            MainPage.Instance.ShowClassesForCurrentDay();
        }
    }
}
