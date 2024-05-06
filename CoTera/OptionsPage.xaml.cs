using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class OptionsPage : ContentPage
    {
        internal static OptionsViewModel? Instance;

        public OptionsPage()
        {
            InitializeComponent();

            Instance = new OptionsViewModel();
            BindingContext = Instance;
            
            DataLoaderSystem.GetAllYears();
        }

        async void OnLegalInformationClick(object sender, EventArgs e) =>
            await DisplayAlert(
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialnoœci za nieprawid³owe dzia³anie aplikacji, poniewa¿ nieistnieje.",
                "OK"
            );

        void OnSaveAndReturn(object sender, EventArgs e)
        {
            //td add load data for chosen year and lab
            Instance!.SaveDataToLoader();
            NavigationSystem.GoBackToMainAsync();
        }

    }
}