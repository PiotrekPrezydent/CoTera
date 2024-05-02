using CoTera.Views;
using Newtonsoft.Json.Linq;
using Octokit;

namespace CoTera.Systems
{
    internal static class DataLoaderSystem
    {
        //$("meta[name=octolytics-dimension-repository_id]").getAttribute('content')
        internal const long REPOID = 793261339;

        internal static string SavedSelectedYear;

        internal static string SavedSelectedLab;

        internal static GitHubClient GitClient;

        internal static string LoadedJsonFile = "";

        internal static void InitializeGitConnection()
        {
            if (GitClient != null)
                return;

            string token = "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";
            GitClient = new GitHubClient(new ProductHeaderValue("GettingDataFromGitDB"));
            GitClient.Credentials = new Credentials(token);
        }

        internal static async void LoadSavedOrDefaultData()
        {
            //TD check if there is any saved data, if so load from file not from git client
            await LoadDefaultData();
        }

        internal static async void GetAllYears()
        {
            await FetchAllYears();
        }

        internal static async void GetAllLabsForCurrentYear()
        {
            await FetchLabsForCurrentYear();
        }
        internal static async void LoadSelectedLabContent()
        {
            await FetchSelectedLabContent();
        }

        static async Task FetchAllYears()
        {
            var loadedData = new List<string>();

            var contents = await GitClient.Repository.Content.GetAllContents(REPOID, "PlanyZajec");

            foreach (var year in contents)
                if (year.Type == ContentType.Dir)
                    loadedData.Add(year.Name);

            OptionsPage.Instance.LoadedYears = loadedData;

            //check if user selected any year previously, if so load that data
            OptionsPage.Instance.SelectedYearIndex = SavedSelectedYear == null ? 0 : loadedData.IndexOf(SavedSelectedYear);
        }

        static async Task LoadDefaultData()
        {
            InitializeGitConnection();
            string selectedLabPath = "PlanyZajec/ExampleFolder/ExLab1.json";
            var request = await GitClient.Repository.Content.GetAllContents(REPOID, selectedLabPath);
            LoadedJsonFile = request[0].Content;
            GenerateAppDataBasedOnLoadedJsonFile();
        }

        static async Task FetchLabsForCurrentYear()
        {
            if (OptionsPage.Instance.SelectedYearIndex == -1 || OptionsPage.Instance.LoadedYears[0] == "-")
                return;

            string selectedYearPath = "PlanyZajec/" + OptionsPage.Instance.LoadedYears[OptionsPage.Instance.SelectedYearIndex];

            var loadedData = new List<string>();

            var contents = await GitClient.Repository.Content.GetAllContents(REPOID, selectedYearPath);

            foreach(var lab in contents)
            {
                if (lab.Type == ContentType.File)
                    loadedData.Add(lab.Name.Substring(0,lab.Name.IndexOf(".json")));
            }

            OptionsPage.Instance.LoadedLabs = loadedData;
            OptionsPage.Instance.SelectedLabIndex = SavedSelectedLab == null ? 0 : loadedData.IndexOf(SavedSelectedLab);
        }

        static async Task FetchSelectedLabContent()
        {
            string selectedLabPath = "PlanyZajec/" + SavedSelectedYear + "/"+ SavedSelectedLab + ".json";
            var request = await GitClient.Repository.Content.GetAllContents(REPOID, selectedLabPath);
            LoadedJsonFile = request[0].Content;
            GenerateAppDataBasedOnLoadedJsonFile();
        }

        static async Task GenerateAppDataBasedOnLoadedJsonFile()
        {
            var parsedJson = JObject.Parse(LoadedJsonFile);

            for (int i = 0; i < 5; i++)
            {
                var rawClassesViews = parsedJson.Values().ToArray()[i];

                List<ClassView> classes = new List<ClassView>();
                foreach (var rawClass in rawClassesViews)
                    classes.Add(new ClassView(rawClass["nazwa"].ToString(), rawClass["godziny"].ToString()));

                MainPage.Instance.LoadedDays[i] = new DayView((DayOfWeek)i + 1, classes.ToArray());
            }

            //refresh showed classes after loading them
            MainPage.Instance.ShowClassesForCurrentDay();
        }

    }
}
