using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class OptionsPage : ContentPage
    {
        internal static OptionsViewModel Instance;

        internal static Picker YearPicker;

        internal static Picker LabPicker;
        public OptionsPage()
        {
            InitializeComponent();
            YearPicker = SelectYear;
            LabPicker = SelectLab;

            Instance = new OptionsViewModel();
        }

        async void OnLegalInformationClick(object sender, EventArgs e) =>
            await DisplayAlert(
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialnoœci za nieprawid³owe dzia³anie aplkikacji\nPoniewa¿ nieistnieje ",
                "OK"
            );

        void OnSaveAndReturn(object sender, EventArgs e)
        {
            //td add load data for chosen year and lab
            Instance.SaveDataToLoader();
            NavigationSystem.GoBackToMainAsync();
        }

        async void OnSelectedYearChanged(object sender, EventArgs e)
        {
            //fetch labs for selected year
        }
    }
}