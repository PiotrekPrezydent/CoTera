using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoTera
{
    internal class CollageDaysLoader
    {
        internal static async void SetCollageDays()
        {
            await FetchDaysFromJson();
            
        }
        static async Task FetchDaysFromJson()
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

            for(int i = 0; i < 5; i++)
            {
                var rawClassesInDay = jsonResponse.Values().ToArray()[i];

                List<CollegeClass> classesInDay = new List<CollegeClass>();
                foreach(var rawClass in rawClassesInDay)
                    classesInDay.Add(new CollegeClass(rawClass["nazwa"].ToString(), rawClass["godziny"].ToString()));

                MainPage.CollageDays[i] = new CollageDay((DayOfWeek)i+1, classesInDay.ToArray());
            }
            MainPage.ShowClassesForCurrentDay();

        }
    }
}
