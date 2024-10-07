using CoTera.Systems;
using CoTera.ViewModels;
using URAPI;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel? Instance;
        internal static int i = 0;
        
        public MainPage()
        {
            InitializeComponent();
            Instance = new MainViewModel();
            BindingContext = Instance;
            Grid grid = (Grid)Content;
            grid.ChildAdded += (a, b) =>
            {
                DisplayAlert("T", (a == AppControllerSystem.LoadingLabel).ToString(), "C");
            };
            grid.Children.Add(AppControllerSystem.LoadingFrame);


            Client.OnGetCollagesStarted += () =>
            {
                //AppControllerSystem.Alert("A", "B", "C");
                //AppControllerSystem.LoadingLabel.Text = "Loading Collages, please wait...";
                //AppControllerSystem.LoadingFrame.IsVisible = true;
            };

            Collage.OnGetMajorsStarted += () =>
            {
                //AppControllerSystem.LoadingLabel.Text = "Loading Majors, please wait...";
                //AppControllerSystem.LoadingFrame.IsVisible = true;
            };

            Major.OnGetYearOfStudiesStarted += () =>
            {
                //AppControllerSystem.LoadingLabel.Text = "Loading Years of studies, please wait...";
                //AppControllerSystem.LoadingFrame.IsVisible = true;
            };

            //Client.OnGetCollagesEnded += () => AppControllerSystem.LoadingFrame.IsVisible = false;
            //Collage.OnGetMajorsEnded += () => AppControllerSystem.LoadingFrame.IsVisible = false;
            //Major.OnGetYearOfStudiesEnded += () => AppControllerSystem.LoadingFrame.IsVisible = false;

            DataLoaderSystem.Initialize();
        }

        void OnPreviousClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(-1);

        void OnNextClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(1);

        void OnOptionsClicked(object sender, EventArgs e) => AppControllerSystem.GoToOptionsAsync();

        void OnRefreshClicked(object sender, EventArgs e)
        {
            var currentPage = Shell.Current.CurrentPage;
            if (currentPage is ContentPage contentPage)
            {
                if (contentPage.Content is Grid grid)
                {
                    if (!grid.Children.Contains(AppControllerSystem.LoadingFrame))
                    {
                        Grid.SetColumn(AppControllerSystem.LoadingFrame, 1);
                        Grid.SetRow(AppControllerSystem.LoadingFrame, 1);
                        grid.Children.Add(AppControllerSystem.LoadingFrame);
                    }
                    else
                    {
                        (grid.Children[grid.Children.IndexOf(AppControllerSystem.LoadingFrame)] as Frame)!.IsVisible = true;
                    }

                }
            }
            AppControllerSystem.GoToOptionsAsync();
        }


        async void Test()
        {
            string msg = "";
            //this dont work check ur api
            var z = await Client.GetCollages();
            msg += z.Count;

            AppControllerSystem.Alert("T", msg, "C");
        }
    }
}