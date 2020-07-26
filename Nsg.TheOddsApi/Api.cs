using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nsg.TheOddsApi
{
    public class TheOddsApi : IRequestFactory
    {

        public static TheOddsApi Current = new TheOddsApi();

        public TheOddsApi()
        {
            this.BaseUrl = "https://api.the-odds-api.com/";
        }

        public string ApiKey { get; set; }

        public string BaseUrl { get; set; }

        public Request CreateRequest()
        {
            Request result = new Request();

            result.BaseUrl = this.BaseUrl;
            result.Parameters.Add("apiKey", ApiKey);

            return result;
        }

        public Task<List<Sport>> GetSports()
        {
            Request Request = CreateRequest();

            Request.HttpMethod = HttpMethod.Get;
            Request.Resource = "v3/sports";


            return Request.Execute<OddsResult<List<Sport>>>().ContinueWith((x) => { return x.Result.Data; });
            //return Request.Execute<OddsResult<List<Sport>>>().Data;

        }

        public Task<List<Match>> GetMatches(string SportKey, Region Region, Market Market)
        {
            Request Request = CreateRequest();

            Request.HttpMethod = HttpMethod.Get;
            Request.Resource = "v3/odds";
            Request.Parameters.Add("sport", SportKey);
            Request.Parameters.Add("region", Region.ToString());
            Request.Parameters.Add("mkt", Market.ToString());

            return Request.Execute<OddsResult<List<Match>>>().ContinueWith((x) => { return x.Result.Data; });
            //return Request.Execute<OddsResult<List<Match>>>().Data;
        }
    }
}
