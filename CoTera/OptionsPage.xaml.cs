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

        void OnLegalInformationClick(object sender, EventArgs e) =>
            AppControllerSystem.Alert(
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialno�ci za nieprawid�owe dzia�anie aplikacji, poniewa� nieistnieje.",
                "OK"
            );

        void OnSaveAndReturn(object sender, EventArgs e)
        {
            //td add load data for chosen year and lab
            Instance!.SaveDataToLoader();
            AppControllerSystem.GoBackToMainAsync();
        }

    }
}