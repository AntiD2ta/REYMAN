using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REYMAN.Models;
using Microsoft.AspNetCore.Authorization;
using BizLogic.Administration;
using Microsoft.AspNetCore.Identity;
using BizData.Entities;
using ServiceLayer.AdminServices;
using BizDbAccess.GenericInterfaces;

namespace REYMAN.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _context;
        public HomeController(UserManager<Usuario> userManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Welcome()
        {
            UserViewModel a = new UserViewModel();
            var principal = await _userManager.GetUserAsync(User);
            a.Name = principal.FirstName;
            a.LastName = principal.FirstLastName;
            return View(a);
        }
        public IActionResult Provincia()
        {
            AdminService ad = new AdminService(_context);
            
            return View(ad.GetProvincias());

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
