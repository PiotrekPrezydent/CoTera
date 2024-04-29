using CoTera.Systems;
using CoTera.ViewModels;
using CoTera.Views;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel Instance;
        internal static CollectionView Classes;

        public MainPage()
        {
            InitializeComponent();

            Classes = MainCollection;
            Instance = new MainViewModel();
            BindingContext = Instance;
            //load 
            DataLoaderSystem.LoadDaysFromDB();
            
        }

        void OnPreviousClicked(object sender, EventArgs e) => Instance.ChosenDay -= 1;

        void OnNextClicked(object sender, EventArgs e) => Instance.ChosenDay += 1;

        void OnOptionsClicked(object sender, EventArgs e)
        {
            NavigationSystem.GoToOptionsAsync();
        }
    }
}