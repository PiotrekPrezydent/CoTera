using CoTera.Systems;
using CoTera.ViewModels;
using URAPI;

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
            Task.Run(async () =>
            {
                Instance.Collages = await Client.GetCollages();
            }).Wait();
            Instance.SelectedCollageIndex = 0;
        }

        void OnLegalInformationClick(object sender, EventArgs e) =>
            AppControllerSystem.Alert(
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialnoœci za nieprawid³owe dzia³anie aplikacji, poniewa¿ nieistnieje.",
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