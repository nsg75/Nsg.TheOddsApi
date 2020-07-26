using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nsg.TheOddsApi
{
    public static class TheOddsApi 
    {
        private static readonly string BaseUrl = "https://api.the-odds-api.com/";

        public static string ApiKey { get; set; }

        private static Request CreateRequest()
        {
            Request result = new Request();

            result.BaseUrl = BaseUrl;
            result.Parameters.Add("apiKey", ApiKey);

            return result;
        }

        public static Task<List<Match>> GetMatches(string SportKey, Region Region, Market Market)
        {
            Request Request = CreateRequest();

            Request.HttpMethod = HttpMethod.Get;
            Request.Resource = "v3/odds";
            Request.Parameters.Add("sport", SportKey);
            Request.Parameters.Add("region", Region.ToString());
            Request.Parameters.Add("mkt", Market.ToString());

            return Request.Execute<OddsResult<List<Match>>>().ContinueWith((x) => { return x.Result.Data; });

        }

        public static Task<List<Sport>> GetSports()
        {
            Request Request = CreateRequest();

            Request.HttpMethod = HttpMethod.Get;
            Request.Resource = "v3/sports";

            return Request.Execute<OddsResult<List<Sport>>>().ContinueWith((x) => { return x.Result.Data; });

        }

        public static List<Match> SimpleOddsAnalyze(List<Match> Matches, string SiteKey1, string SiteKey2)
        {
            
            foreach (Match match in Matches)
            {
                if (HasSite(match, SiteKey1) && HasSite(match, SiteKey2))
                {
                    //match.BestOdds= CompareOdds();
                }
                else
                {
                    match.BestOdds = null;
                }

            }
                

            return Matches;
        }

        private static bool HasSite(Match Match, string SiteKey)
        {
            return Match.Sites.Find((a) => a.SiteKey == SiteKey) != null;
        }

        private static Site CompareOdds(Site Site1, Site Site2)
        {
            throw new NotImplementedException();
        }
    }
}
