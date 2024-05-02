using CoTera.Systems;
using System.ComponentModel;

namespace CoTera.ViewModels
{
    internal class OptionsViewModel
    {
        public OptionsViewModel()
        {
            DataLoaderSystem.GetAllYears();
        }


        internal void SaveDataToLoader()
        {
            DataLoaderSystem.SelectedYearIndex = OptionsPage.YearPicker.SelectedIndex;


            MainPage.DEBUG = DataLoaderSystem.SelectedYearIndex.ToString();
            //DataLoaderSystem.SelectedLabReference = OptionsPage.YearPicker.ItemsSource.IndexOf(SelectedYear);

            MainPage.Instance.ShowClassesForCurrentDay();
        }
    }
}
