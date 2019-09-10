using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace FantasyFootballNotifications
{
    public  class Function1
    {
        private List<string> _myTeams = new List<string>
        {
            "ARI",
            "ATL",
            "DAL",
            "DET",
            "GB",
            "JAX",
            "KC",
            "LAC",
            "MIN",
            "NE",
            "NO",
            "NYG",
            "TEN"
        };

        [FunctionName("Function1")]
        public  void Run([TimerTrigger("0 11 * * * *")]TimerInfo myTimer, ILogger log)
        {
            //GetAllTeams();

            TwilioClient.Init(Environment.GetEnvironmentVariable("TwilioSID"), Environment.GetEnvironmentVariable("TwilioAuthToken"));

            var jsonScheduleData = MakeHttpRequest("https://api.sportsdata.io/v3/nfl/scores/json/Schedules/2019").Result;

            var scheduleData = JsonConvert.DeserializeObject<IEnumerable<ScheduleData>>(jsonScheduleData);

            var myTeamPlays = CheckScheduleForMyPlayersTeams(scheduleData);

            if (myTeamPlays)
            {
                MessageResource.Create(to: new PhoneNumber("555-555-5555"),
                    from: new PhoneNumber("555-555-5555"),
                    body: "You have a fantasy football player playing today.  Please check your lineup!");
            }

        }

        public bool CheckScheduleForMyPlayersTeams(IEnumerable<ScheduleData> scheduleData)
        {
            var todaysGames = scheduleData.Where(sd => sd.Day == DateTime.Today);
            if (!todaysGames.Any())
                return false;

            var awayTeamPlays = _myTeams.Intersect(scheduleData.Select(sd => sd.AwayTeam)).Any();
            var homeTeamPlays = _myTeams.Intersect(scheduleData.Select(sd => sd.HomeTeam)).Any();

            if (awayTeamPlays || homeTeamPlays)
                return true;

            return false;
        }

        public  async Task<string> MakeHttpRequest(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("SportDataApiKey"));
            var res = await httpClient.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            return content;
        }

        //public async void GetAllTeams()
        //{
        //    var res = await MakeHttpRequest("https://api.sportsdata.io/v3/nfl/scores/json/Teams");
        //    var teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(res);

        //    var lstTeams = new List<string>();
        //    foreach (var team in teams)
        //    {
        //        Console.WriteLine($"\"{team.Key}\",");
        //    }

        //    Console.ReadLine();
        //}

    }
}
