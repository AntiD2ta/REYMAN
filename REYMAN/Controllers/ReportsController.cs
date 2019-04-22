using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using BizLogic.Reports;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REYMAN.Controllers
{
    [Authorize("LevelOneAuth")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ReportsController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly GetterUtils _getterUtils;

        public ReportsController(UserManager<Usuario> userManager,
                                 IUnitOfWork context,
                                 IGetterUtils getterUtils)
        {
            _userManager = userManager;
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
        }

        public IActionResult ExportReportOne(ReportOne report)
        {
            var fileContent = new ExportReport2().Generate(report);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContent,
                contentType: "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{report.año}_Report1.xlsx"
                );
        }

        public IActionResult ExportReportTwo(ReportTwo report)
        {
            var fileContent = new ExportReport2().Generate(report);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContent,
                contentType: "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{report.año}_Report2.xlsx"
                );
        }

        public IActionResult ExportReportOne(ReportFour report)
        {
            var fileContent = new ExportReport4().Generate(report);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContent,
                contentType: "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{report.año}_Report4.xlsx"
                );
        }

        public IActionResult ExportReportFive(ReportFive report)
        {
            var fileContent = new ExportReport5().Generate(report);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContent,
                contentType: "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{report.año}_Report5.xlsx"
                );
        }

        public async Task<IActionResult> PickUnidadOrganizativa(ReportViewModel rvm)
        {
            if (User.HasClaim("Permission", "admin"))
            {
                rvm.UOs = new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>;
                return View(rvm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                rvm.UOs = new List<UnidadOrganizativa>() { user.UnidadOrganizativa };
                return RedirectToAction("PickInmueble", rvm);
            }
        }

        public IActionResult PickInmueble(ReportViewModel rvm)
        {
            IEnumerable<Inmueble> inm = new List<Inmueble>();

            foreach (var unidad in rvm.UOs)
                inm = inm.Concat(unidad.Inmuebles);
            
            
            return View(rvm);
        }
    }
}
