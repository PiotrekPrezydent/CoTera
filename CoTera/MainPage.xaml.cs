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
#if ANDROID
            Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("PdfViewer", (handler, View) =>
            {
                handler.PlatformView.Settings.AllowFileAccess = true;
                handler.PlatformView.Settings.AllowFileAccessFromFileURLs = true;
                handler.PlatformView.Settings.AllowUniversalAccessFromFileURLs = true;
            });

            PdfViewer.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file:///android_asset/{WebUtility.UrlEncode(Path.Combine(FileSystem.CacheDirectory + "/savedpdf.pdf"))}";
#else
            PdfViewer.Source = Path.Combine(FileSystem.CacheDirectory + "/savedpdf.pdf");
#endif
        }


        void OnOptionsClicked(object sender, EventArgs e) => AppControllerSystem.GoToOptionsAsync();

        void OnRefreshClicked(object sender, EventArgs e) => DataLoaderSystem.RefreshData();
    }
}