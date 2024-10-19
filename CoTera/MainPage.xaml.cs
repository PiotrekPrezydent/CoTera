using CommunityToolkit.Maui.Views;
using CoTera.Systems;
using CoTera.ViewModels;
using System.Net;
using URAPI;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel Instance = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = Instance;
            SetPage();
        }
        public void SetPage()
        {
            MyImage.Source = DataLoaderSystem.PdfPath;
        }

        void OnOptionsClicked(object sender, EventArgs e) 
        {
            AppControllerSystem.GoToOptionsAsync();

        }

        async void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadingPopup l = new LoadingPopup("Pobieranie planu zajęć i zapisywanie...");
            this.ShowPopup(l);
            await DataLoaderSystem.RefreshData();
            l.Close();
            SetPage();
        }

    }
}