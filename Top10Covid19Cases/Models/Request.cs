using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Models
{
    public class Request
    {
        public HttpRequestMessage request { get; set; }

        public Task<HttpRequestMessage> GetRequest(string uri)
        {
            request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    { "x-rapidapi-key", "467bd71c51mshbcc220dc71b6648p10f4cdjsn81253706d5e8" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                },
            };
            return Task.FromResult(request);
        }
    }
}
