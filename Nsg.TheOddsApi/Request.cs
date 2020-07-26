using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nsg.TheOddsApi
{
    public class Request
    {
        public static readonly HttpClient Client = new HttpClient();

        public string BaseUrl { get; set; }
        public object Data { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public string Resource { get; set; }
        public object ResultType { get; set; }

        private Task<HttpResponseMessage> Execute()
        {
            string Uri = BaseUrl;
            Task<HttpResponseMessage> Response;
            HttpContent Content = null;

            Uri += Resource;
            Uri = ResolveParameters(Uri);

            if (Data != null)
            {
                Content = new StringContent(JsonSerializer.Serialize(Data), Encoding.UTF8);
            }

            if (HttpMethod == HttpMethod.Get)
            {
                Response = Client.GetAsync(Uri);
            }
            else if (HttpMethod == HttpMethod.Post)
            {
                if (Content == null)
                {
                    Content = new StringContent(JsonSerializer.Serialize(Data), Encoding.UTF8);
                }
                Response = Client.PostAsync(Uri, Content);

            }
            else if (HttpMethod == HttpMethod.Put)
            {
                Response = Client.PutAsync(Uri, Content);
            }
            else if (HttpMethod == HttpMethod.Delete)
            {
                Response = Client.DeleteAsync(Uri);
            }
            else
            {
                throw new NotImplementedException();
            }

            return Response;
        }

        public Task<TResult> Execute<TResult>()
        {
            Task<HttpResponseMessage> Response = Execute();

            return Response.ContinueWith((x) =>
            {
                return JsonSerializer.Deserialize<TResult>(x.Result.Content.ReadAsStringAsync().Result);
            });
        }

        private string ResolveParameters(string Uri)
        {
            string Result;

            Result = Uri;

            foreach (KeyValuePair<string, object> Parameter in Parameters)
            {
                if (Result.Contains("?"))
                {
                    Result += "&" + Parameter.Key + "=" + WebUtility.UrlEncode(Parameter.Value.ToString());
                }
                else
                {
                    Result += "?" + Parameter.Key + "=" + WebUtility.UrlEncode(Parameter.Value.ToString());
                }
            }

            return Result;

        }
    }
}
