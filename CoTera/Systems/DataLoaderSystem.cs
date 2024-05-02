using CoTera.Views;
using Newtonsoft.Json.Linq;
using Octokit;

namespace CoTera.Systems
{
    internal static class DataLoaderSystem
    {
        //$("meta[name=octolytics-dimension-repository_id]").getAttribute('content')
        internal const long REPOID = 793261339;

        internal static int SavedSelectedYearIndex = -1;

        internal static int SelectedLabIndex = -1;

        internal static async void LoadDataFromDB()
        {
            await FetchDataFromJson();
        }

        internal static async void GetAllYears()
        {
            await FetchAllYears();
        }

        internal static async void GetAllLabsForCurrentYear()
        {

        }

        static async Task FetchAllYears()
        {
            var loadedData = new List<string>();
            var git = new GitHubClient(new ProductHeaderValue("GetAllPlanyZajec"));
            var contents = await git.Repository.Content.GetAllContents(REPOID, "PlanyZajec");

            foreach (var a in contents)
                if (a.Type == ContentType.Dir)
                    loadedData.Add(a.Name);

            OptionsPage.Instance.LoadedYears = loadedData;

            //check if user selected any year previously, if so load that data
            OptionsPage.Instance.SelectedYearIndex = SavedSelectedYearIndex == -1 ? 0 : SavedSelectedYearIndex;
        }

        static async Task FetchLabsForCurrentYear()
        {
        }

        static async Task FetchDataFromJson()
        {
            string url = "https://raw.githubusercontent.com/typicode/lowdb/main/package.json";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string json = "";
            if (!response.IsSuccessStatusCode)
            {
                //Handle no internet
                return;
            }
            else
                json = await response.Content.ReadAsStringAsync();

            //hard coded sample
            json = "{\r\n    \"PON\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"91:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"WT\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"SR\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"CZW\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"PT\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"SB\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ],\r\n      \"ND\": [\r\n        { \r\n            \"nazwa\": \"Object 1\", \r\n            \"godziny\": \"9:00 AM - 5:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 2\", \r\n            \"godziny\": \"10:00 AM - 6:00 PM\" \r\n        },\r\n        { \r\n            \"nazwa\": \"Object 3\", \r\n            \"godziny\": \"11:00 AM - 7:00 PM\" \r\n        }\r\n      ]\r\n}";

            var jsonResponse = JObject.Parse(json);

            for (int i = 0; i < 5; i++)
            {
                var rawClassesViews = jsonResponse.Values().ToArray()[i];

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
