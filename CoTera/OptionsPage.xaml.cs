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
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialno�ci za nieprawid�owe dzia�anie aplkikacji\nPoniewa� nieistnieje ",
                "OK"
            );

        void OnSaveAndReturn(object sender, EventArgs e)
        {
            //td add load data for chosen year and lab
            Instance!.SaveDataToLoader();
            NavigationSystem.GoBackToMainAsync();
        }

        async void AlertNoInternetConnection() => 
            await DisplayAlert("Brak po��czenia z internetem", 
                "Aplikacja niemog�a wykona� akcji poniewa� niewykryto po��czenie z internetem\n Je�eli problem b�dzie wyst�powa� mimo to prosz� skontaktowa� si� z administratorem aplikacji", 
                "OK"
            );
    }
}