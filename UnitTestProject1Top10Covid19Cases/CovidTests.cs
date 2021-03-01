using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Top10Covid19Cases.Models;

namespace UnitTestProject1Top10Covid19Cases
{
    [TestClass]
    public class CovidTests
    {
        [TestMethod]
        public async Task ListRegionsNotNull()
        {
            DatosRegion datosRegion = new DatosRegion();
            List<Region> regions = await  datosRegion.GetRegions();

            int result = regions.Count > 0 ? 1 : 0;

            Assert.AreEqual(1, result, 0.001,"Regions list is empty");
        }

        [TestMethod]
        public async Task ListReportRegionsNotNull()
        {
            DatosReport datosReport = new DatosReport();
            List<Report> reports = await datosReport.GetReportRegions();

            int result = reports.Count > 0 ? 1 : 0;

            Assert.AreEqual(1, result, 0.001, "Report regions list is empty");
        }

        [TestMethod]
        public async Task ListReportRegionsWithProvinceNotNull()
        {
            DatosReport datosReport = new DatosReport();
            List<Report> reports = await datosReport.GetReportRegionWithProvince("USA");

            int result = reports.Count > 0 ? 1 : 0;

            Assert.AreEqual(1, result, 0.001, "Report regions with province list is empty");
        }

        [TestMethod]
        public async Task ObjectRequestNotNull()
        {
            Request request = new Request();
            string uri = "https://covid-19-statistics.p.rapidapi.com/regions";

            HttpRequestMessage req = await request.GetRequest(uri);

            int result = req != null ? 1 : 0;

            Assert.AreEqual(1, result, 0.001, "Request is null");
        }
    }
}
