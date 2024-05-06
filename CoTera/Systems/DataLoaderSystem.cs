using CoTera.Views;
using Newtonsoft.Json.Linq;
using Octokit;
using Page = Microsoft.Maui.Controls.Page;

namespace CoTera.Systems
{
    //TD: Optimize git connection so app dont use token
    //Optimize checking if there is internet connection, maybe call alert if there is no
    //Move all string to const
    internal static class DataLoaderSystem
    {
        //$("meta[name=octolytics-dimension-repository_id]").getAttribute('content')
        internal const long REPOID = 793261339;

        internal static string? SavedSelectedYear;

        internal static string? SavedSelectedLab;

        internal static GitHubClient? GitClient = new GitHubClient(new ProductHeaderValue("GettingDataFromGitDB"));
        internal static string LoadedJsonFile = "";

        internal static List<string>? LoadedWeeksTypeA;

        static void SaveDataToCache()
        {
            string MainDataFile = Path.Combine(FileSystem.CacheDirectory + "/CoTera_SavedData.txt");

            string savedData = "{Y}=" + SavedSelectedYear + "\n";
            savedData += "{L}=" + SavedSelectedLab + "\n";
            savedData += "{JSON}\n" + LoadedJsonFile;

            File.WriteAllText(MainDataFile, savedData);
        }

        static void GenerateAppDataBasedOnLoadedJsonFile()
        {
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

        internal static async void LoadSavedOrDefaultData()
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
                    Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                    return;
                }

                await FetchAndSetJsonFileContentFromLink("PlanyZajec/PrzykladowyRok/DomyslnaGrupa.json");

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
                    Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                    return;
                }

                await FetchWeeksTypeA();

                string savedData = "";
                for(int i=0;i< LoadedWeeksTypeA!.Count; i++)
                    savedData += LoadedWeeksTypeA[i] +"\n";

                File.WriteAllText(WeeksDataFile,savedData);
            }

            MainPage.Instance!.ChosenDate = DateTime.Now;

            GenerateAppDataBasedOnLoadedJsonFile();
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

        internal static async void Alert(string title, string message, string cancel) => await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);

        static async Task FetchAllYears()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            var loadedData = new List<string>();
            try
            {
                var contents = await GitClient!.Repository.Content.GetAllContents(REPOID, "PlanyZajec");

                foreach (var year in contents)
                    if (year.Type == ContentType.Dir)
                        loadedData.Add(year.Name);

                OptionsPage.Instance!.LoadedYears = loadedData;

                //check if user selected any year previously, if so load that data
                OptionsPage.Instance.SelectedYearIndex = loadedData.IndexOf(SavedSelectedYear!) != -1 ? loadedData.IndexOf(SavedSelectedYear!) : 0;
            }
            catch
            {
                Alert("Limit zapytań", "Wykorzystano limit zapytań dla twojego IP, proszę spróbować ponownie za godzinę.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
            }

        }

        static async Task FetchLabsForCurrentYear()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            if (OptionsPage.Instance!.SelectedYearIndex == -1 || OptionsPage.Instance.LoadedYears[0] == "-")
                return;

            string selectedLabPath = "PlanyZajec/" + OptionsPage.Instance.LoadedYears[OptionsPage.Instance.SelectedYearIndex];

            var loadedData = new List<string>();
            try
            {
                var contents = await GitClient!.Repository.Content.GetAllContents(REPOID, selectedLabPath);

                foreach (var lab in contents)
                    if (lab.Type == ContentType.File)
                        loadedData.Add(lab.Name.Substring(0, lab.Name.IndexOf(".json")));

                OptionsPage.Instance.LoadedLabs = loadedData;
                OptionsPage.Instance.SelectedLabIndex = loadedData.IndexOf(SavedSelectedLab!) != -1 ? loadedData.IndexOf(SavedSelectedLab!) : 0;
            }
            catch
            {
                Alert("Limit zapytań", "Wykorzystano limit zapytań dla twojego IP, proszę spróbować ponownie za godzinę.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
            }

        }
        static async Task FetchSelectedOptionsData()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            await FetchAndSetJsonFileContentFromLink("PlanyZajec/" + SavedSelectedYear + "/" + SavedSelectedLab + ".json");

            SaveDataToCache();
            GenerateAppDataBasedOnLoadedJsonFile();
        }

        static async Task FetchWeeksTypeA()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }

            try
            {
                var request = await GitClient!.Repository.Content.GetAllContents(REPOID, "Tygodnie/Tygodnie_A.json");
                LoadedWeeksTypeA = JArray.Parse(request[0].Content).Select(e => e.ToString()).ToList();
            }
            catch
            {
                Alert("Limit zapytań", "Wykorzystano limit zapytań dla twojego IP, proszę spróbować ponownie za godzinę.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
            }

        }

        static async Task FetchAndSetJsonFileContentFromLink(string link)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet && Connectivity.Current.NetworkAccess != NetworkAccess.ConstrainedInternet)
            {
                Alert("Brak połączenia z internetem", "Aplikacja niemogła pobrać potrzebnych danych poniważ brakuje połączenia z internetem.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem", "OK");
                return;
            }


            try
            {
                var request = await GitClient!.Repository.Content.GetAllContents(REPOID, link);
                LoadedJsonFile = request[0].Content;
            }
            catch
            {
                Alert("Limit zapytań", "Wykorzystano limit zapytań dla twojego IP, proszę spróbować ponownie za godzinę.\nJeżeli problem niebędzie ustępować proszę skontaktować się z administratorem","OK");
            }
        }
    }
}