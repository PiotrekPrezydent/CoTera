using CommunityToolkit.Maui.Views;
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
            InitializeValues();
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

        async void InitializeValues()
        {
            AppControllerSystem.LoadingPopup.SetText("£adowanie listy kolegiów...");
            this.ShowPopup(AppControllerSystem.LoadingPopup);
            Instance.Collages = await Client.GetCollages();
            AppControllerSystem.LoadingPopup.Close();

            Instance.SelectedCollageIndex = 0;
        }

        async void CollageIndexChanged(object sender, EventArgs e)
        {
            AppControllerSystem.LoadingPopup.SetText("£adowanie przedmiotów...");
            this.ShowPopup(AppControllerSystem.LoadingPopup);
            Instance.Majors = await Instance.Collages[Instance.SelectedCollageIndex].GetMajors();
            AppControllerSystem.LoadingPopup.Close();
            if (Instance.SelectedMajorIndex == 0)
                MajorIndexChanged(sender, e);

            Instance.SelectedMajorIndex = 0;
        }

        async void MajorIndexChanged(object sender, EventArgs e)
        {
            AppControllerSystem.LoadingPopup.SetText("£adowanie planów zajêæ...");
            this.ShowPopup(AppControllerSystem.LoadingPopup);
            Instance.YearsOfStudies = await Instance.Majors[Instance.SelectedMajorIndex].GetYearOfStudies();
            AppControllerSystem.LoadingPopup.Close();
            Instance.SelectedYearOfStudiesIndex = 0;
        }
    }
}