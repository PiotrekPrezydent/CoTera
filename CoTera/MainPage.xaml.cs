using CoTera.Systems;
using CoTera.ViewModels;
using Newtonsoft.Json.Linq;
using Octokit;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel? Instance;
        public static string DEBUG;

        public MainPage()
        {
            InitializeComponent();
            DEBUG = "";
            Instance = new MainViewModel();
            BindingContext = Instance;

            DataLoaderSystem.LoadSavedOrDefaultData();
        }

        void OnPreviousClicked(object sender, EventArgs e) => Instance.ChosenDate = Instance.ChosenDate.AddDays(-1);

        void OnNextClicked(object sender, EventArgs e) => Instance.ChosenDate = Instance.ChosenDate.AddDays(1);

        void OnOptionsClicked(object sender, EventArgs e) => NavigationSystem.GoToOptionsAsync();

        async void OnRefreshClicked(object sender, EventArgs e)
        {
            await DisplayAlert("DEBUG",DEBUG,"OK");
        }
    }
}