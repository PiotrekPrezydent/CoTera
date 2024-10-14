using CommunityToolkit.Maui.Views;
using CoTera.Systems;
using CoTera.ViewModels;
using URAPI;

namespace CoTera
{
    public partial class OptionsPage : ContentPage
    {
        static internal OptionsViewModel Instance = new OptionsViewModel();

        public OptionsPage()
        {
            InitializeComponent();
            BindingContext = Instance;
            InitializeValues();
        }

        async void OnSaveAndReturn(object sender, EventArgs e)
        {
            DataLoaderSystem.SelectedCollageIndex = Instance.SelectedCollageIndex;
            DataLoaderSystem.SelectedMajorIndex = Instance.SelectedMajorIndex;
            DataLoaderSystem.SelectedScheduleIndex = Instance.SelectedScheduleIndex;

            LoadingPopup l = new LoadingPopup("Pobieranie planu zajêæ i zapisywanie...");
            this.ShowPopup(l);

            await DataLoaderSystem.SaveData();
            l.Close();
            AppControllerSystem.GoBackToMainAsync();
        }

        async void InitializeValues()
        {
            LoadingPopup l = new LoadingPopup("£adowanie listy kolegiów...");
            this.ShowPopup(l);
            Instance.Collages = await Client.GetCollages();
            l.Close();

            Instance.SelectedCollageIndex = DataLoaderSystem.SelectedCollageIndex;
        }

        async void CollageIndexChanged(object sender, EventArgs e)
        {
            LoadingPopup l = new LoadingPopup("£adowanie listy przedmiotów...");
            this.ShowPopup(l);
            Instance.Majors = await Instance.Collages[Instance.SelectedCollageIndex].GetMajors();
            l.Close();
            if (Instance.SelectedMajorIndex == 0)
                MajorIndexChanged(sender, e);

            Instance.SelectedMajorIndex = DataLoaderSystem.SelectedMajorIndex;
        }

        async void MajorIndexChanged(object sender, EventArgs e)
        {
            LoadingPopup l = new LoadingPopup("£adowanie listy planów zajêæ...");
            this.ShowPopup(l);
            Instance.Schedules = await Instance.Majors[Instance.SelectedMajorIndex].GetSchedules();
            l.Close();
            Instance.SelectedScheduleIndex = DataLoaderSystem.SelectedScheduleIndex;
        }


        void OnLegalInformationClick(object sender, EventArgs e) =>
            AppControllerSystem.Alert
            (
                "Informacje Prawne",
                "Firma PiotrPrezydentApps sp. z o. o. nie ponosi odpowiedzialnoœci za nieprawid³owe dzia³anie aplikacji, poniewa¿ nieistnieje.",
                "OK"
            );
    }
}