using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nsg.TheOddsApi
{

    public class OddsResult<TData>
    {

        [JsonPropertyName("data")]
        public TData Data { get; set; }
    }

    public class Sport
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }


    public enum Region
    {
        eu,
        uk,
        us,
        au
    }

    public enum Market
    {
        h2h,
        spreads,
        totals
    }

    public class Match
    {
        [JsonPropertyName("sport_key")]
        public string SportKey { get; set; }

        [JsonPropertyName("sport_nice")]
        public string SportNice { get; set; }

        [JsonPropertyName("teams")]
        public string[] Teams { get; set; }

        [JsonPropertyName("commence_time")]
        public int CommenceTimeTimeStamp { get; set; }

        [JsonPropertyName("home_team")]
        public string HomeTeam { get; set; }

        [JsonPropertyName("sites")]
        public List<Site> Sites { get; set; }

        [JsonPropertyName("sites_count")]
        public int SitesCount { get; set; }

        public DateTime CommenceTime { get { return DateTimeOffset.FromUnixTimeMilliseconds(CommenceTimeTimeStamp).DateTime; } }

        public Site BestOdds { get; set; }
    }

    public class Site
    {
        [JsonPropertyName("site_key")]
        public string SiteKey { get; set; }

        [JsonPropertyName("site_nice")]
        public string SiteNice { get; set; }

        [JsonPropertyName("last_update")]
        public int LastUpdateTimeStamp { get; set; }

        [JsonPropertyName("odds")]
        public Odds Odds { get; set; }

        public DateTime LastUpdate { get { return DateTimeOffset.FromUnixTimeMilliseconds(LastUpdateTimeStamp).DateTime; } }
    }

    public class Odds
    {
        [JsonPropertyName("h2h")]
        public decimal[] H2h { get; set; }

        [JsonPropertyName("spreads")]
        public Spread Spreads { get; set; }

        [JsonPropertyName("totals")]
        public Total Totals { get; set; }
    }

    public class Spread
    {
        [JsonPropertyName("odds")]
        public decimal[] Odds { get; set; }

        [JsonPropertyName("points")]
        public string[] Points { get; set; }
    }

    public class Total
    {
        [JsonPropertyName("odds")]
        public decimal[] Odds { get; set; }

        [JsonPropertyName("points")]
        public decimal[] Points { get; set; }

        [JsonPropertyName("position")]
        public string[] Position { get; set; }
    }

}
