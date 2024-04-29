using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class OptionsPage : ContentPage
    {
        internal static OptionsViewModel Instance;
        public OptionsPage()
        {
            InitializeComponent();
            Instance = new OptionsViewModel();
            SelectYear.SelectedItem = DataLoaderSystem.SelectedYear;
            SelectLab.SelectedItem = DataLoaderSystem.SelectedLab;
        }

        async void OnLegalInformationClick(object sender, EventArgs e) =>
            await DisplayAlert(
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialnoœci za nieprawid³owe dzia³anie aplkikacji\nPoniewa¿ nieistnieje ",
                "OK"
            );

        void OnSelectedYear(object sender, EventArgs e) => Instance.SelectedYear = (string)SelectYear.SelectedItem;

        void OnSelectedLab(object sender, EventArgs e) => Instance.SelectedLab = (string)SelectLab.SelectedItem;

        void OnSaveAndReturn(object sender, EventArgs e)
        {
            //td add load data for chosen year and lab
            Instance.SaveDataToLoader();
            NavigationSystem.GoBackToMainAsync();
        }
    }
}