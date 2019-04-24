using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using BizLogic.Reports;
using Castle.Core.Configuration;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Reports;
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

        public async Task<IActionResult> ExportReportOne(ExportReportOneViewModel export)
        {
            ReportOne report;
            if (User.HasClaim("Permission", "admin"))
            {
                report = new GenerateReport1((EfCoreContext)_context).GenerateReport(export.Año, export.TipoPlan,
                                                                                         (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre),
                                                                                         (new GetterAll(_getterUtils, _context).GetAll("Inmueble") as IEnumerable<Inmueble>).Select(inm => inm.Direccion));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                report = new GenerateReport1((EfCoreContext)_context).GenerateReport(export.Año, export.TipoPlan,
                                                                                         new List<string>() { user.UnidadOrganizativa.Nombre },
                                                                                         user.UnidadOrganizativa.Inmuebles.Select(inm => inm.Direccion));
               
            }

            var fileContent = new ExportReport().ExportReport1(report);

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

        public async Task<IActionResult> ExportReportTwo(ExportReportTwoViewModel export)
        {
            ReportTwo report;
            if (User.HasClaim("Permission", "admin"))
                report = new GenerateReport(_context).GenerateReport2(export.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                report = new GenerateReport(_context).GenerateReport2(export.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
            }

            var fileContent = new ExportReport().ExportReport2(report);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContent,
                contentType: "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{export.Año}_Report2.xlsx"
                );
        }

        public async Task<IActionResult> ExportReportFour(ExportReportFourViewModel export)
        {
            ReportFour report;

            if (User.HasClaim("Permission", "admin"))
                report = new GenerateReport(_context).GenerateReport4(export.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                report = new GenerateReport(_context).GenerateReport4(export.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
            }

            var fileContent = new ExportReport().ExportReport4(report);

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

        public async Task<IActionResult> ExportReportFive(ExportReportFiveViewModel export)
        {
            ReportFive report;
            if (User.HasClaim("Permission", "admin"))
                report = new GenerateReport(_context).GenerateReport5(export.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                report = new GenerateReport(_context).GenerateReport5(export.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
            }

            var fileContent = new ExportReport().ExportReport5(report);

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



        [HttpGet]
        public IActionResult ReportOne()
        {
            return View(new ReportOneViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ReportOne(ReportOneViewModel rvm)
        {
            if (User.HasClaim("Permission", "admin"))
            {
                rvm.Report = new GenerateReport(_context).GenerateReport1(rvm.Año, rvm.TipoPlan,
                                                                          (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre),
                                                                          (new GetterAll(_getterUtils, _context).GetAll("Inmueble") as IEnumerable<Inmueble>).Select(inm => inm.Direccion));
                return View(rvm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                rvm.Report = new GenerateReport(_context).GenerateReport1(rvm.Año, rvm.TipoPlan,
                                                                          new List<string>() { user.UnidadOrganizativa.Nombre },
                                                                          user.UnidadOrganizativa.Inmuebles.Select(inm => inm.Direccion));
                return View(rvm);
            }
        }

        [HttpGet]
        public IActionResult ReportTwo()
        {
            return View(new ReportTwoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ReportTwo(ReportTwoViewModel rvm)
        {
            if (User.HasClaim("Permission", "admin"))
            {
                rvm.Report = new GenerateReport(_context).GenerateReport2(rvm.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));
                                                                            
                return View(rvm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                rvm.Report = new GenerateReport(_context).GenerateReport2(rvm.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
                return View(rvm);
            }
        }

        [HttpGet]
        public IActionResult ReportFour()
        {
            return View(new ReportFourViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ReportFour(ReportFourViewModel rvm)
        {
            if (User.HasClaim("Permission", "admin"))
            {
                rvm.Report = new GenerateReport(_context).GenerateReport4(rvm.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));

                return View(rvm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                rvm.Report = new GenerateReport(_context).GenerateReport4(rvm.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
                return View(rvm);
            }
        }

        [HttpGet]
        public IActionResult ReportFive()
        {
            return View(new ReportFiveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ReportFive(ReportFiveViewModel rvm)
        {
            if (User.HasClaim("Permission", "admin"))
            {
                rvm.Report = new GenerateReport(_context).GenerateReport5(rvm.Año, (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Select(ud => ud.Nombre));

                return View(rvm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                rvm.Report = new GenerateReport(_context).GenerateReport5(rvm.Año, new List<string>() { user.UnidadOrganizativa.Nombre });
                return View(rvm);
            }
        }
    }
}
