using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Demo.CustomLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            // Update port # in the following line.
            client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("api-url"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("api-key"));
                
                //Extract 4 hours for every i
            for (int i = 100; i >=0; i--)
            {
                int x = i * -4;
                DateTime buildDate = DateTime.Now.AddHours(x);
                BuildCompliance bc = new BuildCompliance();
                bc.BuildDateTime = buildDate;
                bc.BuildDefinitionName = GetBuildDefinitionName();
                bc.BuildNumber = $"{buildDate.ToString("yyyyMMdd")}.{i.ToString()}";
                bc.BuildStatus = GetBuildStatus();
                bc.HasFourEyes = GetFourEyes();

                LogAnalyticsClass la = new LogAnalyticsClass();
                la.customlogType = "BuildCompliance";
                la.json = bc;
                string json = JsonConvert.SerializeObject(la);
                Console.WriteLine($"{bc.BuildDefinitionName} - {bc.BuildNumber} - {bc.BuildStatus} - {bc.HasFourEyes} - {bc.BuildDateTime}");
                HttpResponseMessage response = client.PostAsJsonAsync("api/logs", la).Result;
                Console.WriteLine($"Called API: {response.StatusCode}");
                response.EnsureSuccessStatusCode();

            }

            Console.WriteLine($"API-URL: {Environment.GetEnvironmentVariable("api-url")}");
        }

        private static bool GetFourEyes()
        {
            List<bool> ls = new List<bool>();
            ls.Add(true);
            ls.Add(false);

            Random random = new Random();
            int randomNr = random.Next(0, (ls.Count));
            return ls[randomNr];
        }

        private static string GetBuildStatus()
        {
            List<string> ls = new List<string>();
            ls.Add("Succeeded");
            ls.Add("Failed");

            Random random = new Random();
            int randomNr = random.Next(0, ls.Count);
            return ls[randomNr];
        }

        private static string GetBuildDefinitionName()
        {
            List<string> ls = new List<string>();
            ls.Add("CI build MusicStore");
            ls.Add("Release build MusicStore");
            ls.Add("PartsUnlimited CI/CD");

            Random random = new Random();
            int randomNr = random.Next(0,ls.Count);
            return ls[randomNr];
        }
    }
}
