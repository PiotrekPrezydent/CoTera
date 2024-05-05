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

            try { DataLoaderSystem.GetAllYears(); }
            catch { AlertNoInternetConnection(); }

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
            Instance!.SaveDataToLoader();
            NavigationSystem.GoBackToMainAsync();
        }

        async void AlertNoInternetConnection() => 
            await DisplayAlert("Brak po³¹czenia z internetem", 
                "Aplikacja niemog³a wykonaæ akcji poniewa¿ niewykryto po³¹czenie z internetem\n Je¿eli problem bêdzie wystêpowaæ mimo to proszê skontaktowaæ siê z administratorem aplikacji", 
                "OK"
            );
    }
}