using CoTera.Systems;
using CoTera.ViewModels;
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

        async void Tester()
        {
            //$("meta[name=octolytics-dimension-repository_id]").getAttribute('content')
            const long id = 793261339;
            var git = new GitHubClient(new ProductHeaderValue("GetAllPlanyZajec"));
            var contents = await git.Repository.Content.GetAllContents(id,"PlanyZajec");
            foreach(var a in contents)
            {
                if(a.Type == ContentType.Dir)
                {
                    DEBUG += a.Path + " /// " + a.Name + "\n";

                }
            }
        }
    }
}