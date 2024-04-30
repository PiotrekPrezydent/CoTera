using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel Instance;
        internal static CollectionView Classes;
        public static string DEBUG;

        public MainPage()
        {
            InitializeComponent();
            DEBUG = "";
            Classes = MainCollection;
            Instance = new MainViewModel();
            BindingContext = Instance;
            DateTime d = DateTime.Today;
            DEBUG = d.ToString().Substring(0,10) + "\n" + d.AddDays(1).ToString() + "\n" + d.AddDays(-1);
            //load 
            DataLoaderSystem.LoadDataFromDB();

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