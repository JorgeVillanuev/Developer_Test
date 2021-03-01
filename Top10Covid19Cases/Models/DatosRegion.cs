using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Models
{
    public class DatosRegion
    {
        public List<Region> data { get; set; }

        private HttpClient client;

        private Request request;

        private List<Region> ListRegions;

        public DatosRegion()
        {
            request = new Request();
            client = new HttpClient();
        }

        public async Task<List<Region>> GetRegions()
        {
            ListRegions = new List<Region>();

            await request.GetRequest("https://covid-19-statistics.p.rapidapi.com/regions");
            using (var response = await client.SendAsync(request.request))
            {
                response.EnsureSuccessStatusCode();
                var bodyData = await response.Content.ReadAsStringAsync();
                DatosRegion region = JsonConvert.DeserializeObject<DatosRegion>(bodyData);
                ListRegions = region.data.OrderBy(r => r.name).ToList();
            }
            return ListRegions;
        }
    }
}
