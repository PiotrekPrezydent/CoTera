namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static DayOfWeek ChosenDay;
        internal static CollageDay[] CollageDays = new CollageDay[7];
        internal static CollectionView Classes;
        internal static Label DayName;


        public MainPage()
        {
            InitializeComponent();
            Classes = ClassesList;
            DayName = Lab_DOTW;

            InitializeDefaultDays();
            CollageDaysLoader.SetCollageDays();
            //td: handle loading from SetCollageDays

            //this will be Date.Today.DayOfTheWeek
            ChosenDay = DayOfWeek.Monday;
            ShowClassesForCurrentDay();

        }
        internal static void ShowClassesForCurrentDay()
        {
            DayName.Text = ChosenDay.ToString();
            int index = 0;
            if (ChosenDay == DayOfWeek.Sunday)
                index = 7;
            else
                index = (int)ChosenDay;

            index--;

            CollageDay day = CollageDays[index];
            Classes.ItemsSource = day.Classes.Select(e => e.Name + "\n" + e.TimeSpan);
        }

        void OnPreviousClicked(object sender, EventArgs e)
        {
            if (ChosenDay == DayOfWeek.Sunday)
                ChosenDay = DayOfWeek.Saturday;
            else
                ChosenDay -= 1;

            ShowClassesForCurrentDay();
        }

        void OnNextClicked(object sender, EventArgs e)
        {
            if (ChosenDay == DayOfWeek.Saturday)
                ChosenDay = DayOfWeek.Sunday;
            else
                ChosenDay += 1;

            ShowClassesForCurrentDay();
        }

        void InitializeDefaultDays()
        {
            for(int i = 0; i < 7; i++)
            {
                CollegeClass[] classes = { new CollegeClass("UNKNOWN", "UNKNOWN") };
                CollageDays[i] = new CollageDay((DayOfWeek)i, classes);
            }
        }

    }
}
