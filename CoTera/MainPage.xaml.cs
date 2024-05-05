using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel? Instance;

        public MainPage()
        {
            InitializeComponent();
            Instance = new MainViewModel();
            BindingContext = Instance;

            DataLoaderSystem.LoadSavedOrDefaultData();
        }

        void OnPreviousClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(-1);

        void OnNextClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(1);

        void OnOptionsClicked(object sender, EventArgs e) => NavigationSystem.GoToOptionsAsync();

        void OnRefreshClicked(object sender, EventArgs e)
        {
            try { DataLoaderSystem.GetSelectedOptionsContent(); } 
            catch { AlertNoInternetConnection(); }
        }

        async void AlertNoInternetConnection() => await DisplayAlert("Brak połączenia z internetem", "Aplikacja niemogła wykonać akcji ponieważ niewykryto połączenie z internetem\n Jeżeli problem będzie występować mimo to proszę skontaktować się z administratorem aplikacji", "OK");
    }
}