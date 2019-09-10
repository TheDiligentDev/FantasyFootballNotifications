using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballNotifications
{

    public class ScheduleData
    {
        public string GameKey { get; set; }
        public int SeasonType { get; set; }
        public int Season { get; set; }
        public int Week { get; set; }
        public DateTime? Date { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public string Channel { get; set; }
        public float? PointSpread { get; set; }
        public float? OverUnder { get; set; }
        public int? StadiumID { get; set; }
        public bool? Canceled { get; set; }
        public object GeoLat { get; set; }
        public object GeoLong { get; set; }
        public int? ForecastTempLow { get; set; }
        public int? ForecastTempHigh { get; set; }
        public string ForecastDescription { get; set; }
        public int? ForecastWindChill { get; set; }
        public int? ForecastWindSpeed { get; set; }
        public int? AwayTeamMoneyLine { get; set; }
        public int? HomeTeamMoneyLine { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? DateTime { get; set; }
        public int GlobalGameID { get; set; }
        public int GlobalAwayTeamID { get; set; }
        public int GlobalHomeTeamID { get; set; }
        public int ScoreID { get; set; }
        public string Status { get; set; }
        public Stadiumdetails StadiumDetails { get; set; }
    }

    public class Stadiumdetails
    {
        public int StadiumID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Capacity { get; set; }
        public string PlayingSurface { get; set; }
        public float? GeoLat { get; set; }
        public float? GeoLong { get; set; }
        public string Type { get; set; }
    }

}
