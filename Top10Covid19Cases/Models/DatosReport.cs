using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Models
{
    public class DatosReport
    {
        public List<Report> data { get; set; }

        private HttpClient client;

        private Request request;

        private DatosRegion datosRegion;

        private List<Report> ListReport;

        public DatosReport()
        {
            request = new Request();
            datosRegion = new DatosRegion();
            client = new HttpClient();
        }

        public async Task<List<Report>> GetReportRegions()
        {
            ListReport = new List<Report>();

            await request.GetRequest("https://covid-19-statistics.p.rapidapi.com/reports");

            using (var response = await client.SendAsync(request.request))
            {
                response.EnsureSuccessStatusCode();
                var bodyData = await response.Content.ReadAsStringAsync();
                DatosReport report = JsonConvert.DeserializeObject<DatosReport>(bodyData);

                ListReport = (from data in report.data
                              group data by data.region.name into agrupacion
                              select new Report
                              {
                                  region = new Region
                                  {
                                      name = agrupacion.Key,
                                  },
                                  confirmed = report.data.FindAll(l => l.region.name == agrupacion.Key).Sum(l => l.confirmed),
                                  deaths = report.data.FindAll(l => l.region.name == agrupacion.Key).Sum(l => l.deaths),

                              }).OrderByDescending(x => x.confirmed).Take(10).ToList();
            }
            return ListReport;
        }

        public async Task<List<Report>> GetReportRegionWithProvince(string iso)
        {
            Region region = (await datosRegion.GetRegions()).Find(r => r.iso == iso);
            ListReport = new List<Report>();

            await request.GetRequest($"https://covid-19-statistics.p.rapidapi.com/reports?iso={region.iso}&region_name={region.name}");
            using (var response = await client.SendAsync(request.request))
            {
                response.EnsureSuccessStatusCode();
                var bodyData = await response.Content.ReadAsStringAsync();
                DatosReport report = JsonConvert.DeserializeObject<DatosReport>(bodyData);
                ListReport = report.data.OrderByDescending(r => r.confirmed).Take(10).ToList();
            }
            return ListReport;
        }
    }
}
