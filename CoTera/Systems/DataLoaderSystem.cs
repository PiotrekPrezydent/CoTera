using CoTera.Views;
using Newtonsoft.Json.Linq;
using Octokit;

namespace CoTera.Systems
{
    //TD: Optimize git connection, now its called too many times
    //Optimize checking if there is internet connection
    //TD: there is small memory like something like 1MB per changed day, check why
    internal static class DataLoaderSystem
    {
        //$("meta[name=octolytics-dimension-repository_id]").getAttribute('content')
        internal const long REPOID = 793261339;

        internal static string? SavedSelectedYear;

        internal static string? SavedSelectedLab;

        internal static string? LoadedJsonFile;

        internal static List<string>? LoadedWeeksTypeA;

        internal static GitHubClient? GitClient;

        internal static async void Initialize()
        {
            SavedSelectedYear = "";
            SavedSelectedLab = "";
            LoadedJsonFile = "";
            LoadedWeeksTypeA = new List<string>();
            GitClient = new GitHubClient(new ProductHeaderValue("GettingDataFromGitDB"));

            await LoadSavedOrDefaultData();
        }

        internal static async void GetAllYears() => await FetchAllYears();

        internal static async void GetAllLabsForCurrentYear() => await FetchLabsForCurrentYear();

        internal static async void GetSelectedOptionsContent() => await FetchSelectedOptionsData();

        internal static async void RefreshData()
        {
            await FetchSelectedOptionsData();

            await FetchWeeksTypeA();

            string WeeksDataFile = Path.Combine(FileSystem.CacheDirectory + "/CoTera_WeeksTypeA.txt");
            string savedData = "";
            for (int i = 0; i < LoadedWeeksTypeA!.Count; i++)
                savedData += LoadedWeeksTypeA[i] + "\n";

            File.WriteAllText(WeeksDataFile, savedData);
        }

        static void SaveDataToCache()
        {
            string MainDataFile = Path.Combine(FileSystem.CacheDirectory + "/CoTera_SavedData.txt");

            string savedData = "{Y}=" + SavedSelectedYear + "\n";
            savedData += "{L}=" + SavedSelectedLab + "\n";
            savedData += "{JSON}\n" + LoadedJsonFile;

            File.WriteAllText(MainDataFile, savedData);
        }

        static void GenerateDataFromLoadedJsonFile()
        {
            if (LoadedJsonFile == "" || LoadedJsonFile == null)
                return;

            var parsedJson = JObject.Parse(LoadedJsonFile);

            for (int i = 0; i < 5; i++)
            {
                var rawClassesViews = parsedJson.Values().ToArray()[i];

                List<ClassView> classes = new List<ClassView>();
                foreach (var rawClass in rawClassesViews)
                    classes.Add(new ClassView(rawClass));

                MainPage.Instance!.LoadedDays[i] = new DayView((DayOfWeek)i + 1, classes.ToArray());
            }

            //refresh showed classes after loading them
            MainPage.Instance!.ShowClassesForCurrentDay();
        }

        static async Task FetchWeeksTypeA()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            var contents = await GetDataFromRepoPath("Tygodnie/Tygodnie_A.json");
            if (contents == null)
                return;

            LoadedWeeksTypeA = JArray.Parse(contents[0].Content).Select(e => e.ToString()).ToList();
        }

        static async Task FetchAllYears()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            var loadedData = new List<string>();

            var contents = await GetDataFromRepoPath("PlanyZajec");

            if (contents == null)
                return;

            foreach (var year in contents)
                if (year.Type == ContentType.Dir)
                    loadedData.Add(year.Name);

            //OptionsPage.Instance!.Collages = loadedData;
            //OptionsPage.Instance.SelectedCollageIndex = loadedData.IndexOf(SavedSelectedYear!) != -1 ? loadedData.IndexOf(SavedSelectedYear!) : 0;
        }

        static async Task FetchLabsForCurrentYear()
        {
            //if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            //{
            //    AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
            //    return;
            //}

            //if (OptionsPage.Instance!.SelectedCollageIndex == -1 || OptionsPage.Instance.Collages[0] == "-")
            //    return;

            //string selectedLabPath = "PlanyZajec/" + OptionsPage.Instance.Collages[OptionsPage.Instance.SelectedCollageIndex];

            //var loadedData = new List<string>();

            //var contents = await GetDataFromRepoPath(selectedLabPath);

            //if (contents == null)
            //    return;

            //foreach (var lab in contents)
            //    if (lab.Type == ContentType.File)
            //        loadedData.Add(lab.Name.Substring(0, lab.Name.IndexOf(".json")));

            //OptionsPage.Instance.Majors = loadedData;
            //OptionsPage.Instance.SelectedMajorIndex = loadedData.IndexOf(SavedSelectedLab!) != -1 ? loadedData.IndexOf(SavedSelectedLab!) : 0;
        }

        static async Task FetchSelectedOptionsData()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            if (SavedSelectedYear == null || SavedSelectedYear == "" || SavedSelectedLab == null || SavedSelectedLab == "")
                return;

            var contents = await GetDataFromRepoPath("PlanyZajec/" + SavedSelectedYear + "/" + SavedSelectedLab + ".json");
            if (contents == null)
                return;

            LoadedJsonFile = contents[0].Content;

            SaveDataToCache();
            GenerateDataFromLoadedJsonFile();
        }


        static async Task LoadSavedOrDefaultData()
        {
            string MainDataFile = Path.Combine(FileSystem.CacheDirectory + "/CoTera_SavedData.txt");
            string WeeksDataFile = Path.Combine(FileSystem.CacheDirectory + "/CoTera_WeeksTypeA.txt");

            //load saved class plan
            if (File.Exists(MainDataFile))
            {
                string load = File.ReadAllText(MainDataFile);

                SavedSelectedYear = File.ReadAllLines(MainDataFile)[0].Substring(4);
                SavedSelectedLab = File.ReadAllLines(MainDataFile)[1].Substring(4);
                LoadedJsonFile = load.Substring(load.IndexOf("{JSON}") + 6);
            }
            else
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
                {
                    AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                    return;
                }

                var contents = await GetDataFromRepoPath("PlanyZajec/PrzykladowyRok/DomyslnaGrupa.json");
                if (contents == null)
                    return;

                LoadedJsonFile = contents[0].Content;
                SavedSelectedYear = "PrzykladowyRok";
                SavedSelectedLab = "DomyslnaGrupa";

                SaveDataToCache();
            }

            //load saved weeks type a
            if (File.Exists(WeeksDataFile))
                LoadedWeeksTypeA = File.ReadAllLines(WeeksDataFile).ToList();
            else
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
                {
                    AppControllerSystem.Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                    return;
                }

                await FetchWeeksTypeA();

                string savedData = "";
                for(int i=0;i< LoadedWeeksTypeA!.Count; i++)
                    savedData += LoadedWeeksTypeA[i] +"\n";

                File.WriteAllText(WeeksDataFile,savedData);
            }

            MainPage.Instance!.ChosenDate = DateTime.Now;

            GenerateDataFromLoadedJsonFile();
        }

        static async Task<IReadOnlyList<RepositoryContent>> GetDataFromRepoPath(string path)
        {
            try
            {
                var returnedValue = await GitClient!.Repository.Content.GetAllContents(REPOID, path);
                return returnedValue;
            }
            catch
            {
                AppControllerSystem.Alert("Limit zapytań", "Wykorzystano limit zapytań dla twojego IP, proszę spróbować ponownie za godzinę.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return null;
            }
        }
    }
}