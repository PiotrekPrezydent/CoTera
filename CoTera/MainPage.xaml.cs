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
#if ANDROID
            Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("PdfViewer", (handler, View) =>
            {
                handler.PlatformView.Settings.AllowFileAccess = true;
                handler.PlatformView.Settings.AllowFileAccessFromFileURLs = true;
                handler.PlatformView.Settings.AllowUniversalAccessFromFileURLs = true;
            });

            PdfViewer.Source = $"file:///android_asset/pdfjs/web/viewer.html?file={Path.Combine(FileSystem.CacheDirectory + "/savedpdf.pdf")}";
#else
            PdfViewer.Source = Path.Combine(FileSystem.CacheDirectory + "/savedpdf.pdf");
#endif
        }

        void OnOptionsClicked(object sender, EventArgs e) => AppControllerSystem.GoToOptionsAsync();

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