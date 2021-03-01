using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19Cases.Models;

namespace Top10Covid19Cases.Controllers
{
    public class CovidController : Controller
    {
        private DatosRegion datosRegion = new DatosRegion();
        private DatosReport datosReport = new DatosReport();
        //GET
        public async Task<IActionResult> Index()
        {
            List<Region> ListRegions = await datosRegion.GetRegions();
            List<Report> ListReport = await datosReport.GetReportRegions();

            ViewData["ListReports"] = ListReport;
            ViewData["ListRegions"] = new SelectList(ListRegions, "iso", "name");
            ViewData["Regions"] = true;

            return View();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Index(string iso)
        {
            List<Report> ListReport = new List<Report>();
            if (iso.Equals("regions"))
            {
                ListReport = await datosReport.GetReportRegions();

                ViewData["Regions"] = true;
            }
            else
            {
                ListReport = await datosReport.GetReportRegionWithProvince(iso);

                ViewData["Regions"] = false;
            }
            List<Region> ListRegions = await datosRegion.GetRegions();

            ViewData["ListRegions"] = new SelectList(ListRegions, "iso", "name");
            ViewData["ListReports"] = ListReport;

            return View();
        }
    }
}
