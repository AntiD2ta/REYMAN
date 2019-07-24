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
using BizDbAccess.Utils;
namespace REYMAN.Controllers
{
    /// <summary>
    /// Manage all the views related to the main/home page.
    /// </summary>
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUnitOfWork _context;
        private readonly GetterUtils _getterUtils;

        /// <summary>
        /// Constructor for the controller.
        /// </summary>
        /// <param name="userManager">Object of ASP.NET CORE Identity. Is the repository for Usuario entity</param>
        /// <param name="context">Unit of Work in charge of the access to the database. Configured
        /// in Startup/ConfigureServices</param>
        /// <param name="getterUtils">See description for this interface.</param>
        public HomeController(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IUnitOfWork context,
            IGetterUtils getterUtils)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
        }

        /// <summary>
        /// GET method of Index view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (User.HasClaim("Pending", "false"))
            {
                if (Request.Query.Keys.Contains("ReturnUrl"))
                {
                    return Redirect(Request.Query["ReturnUrl"].First());
                }
                else if (User.HasClaim("Permission", "admin"))
                {
                    return Admin();
                }
                else
                {
                    return Admin();
                }
            }
            else
            {
                return RedirectToAction("Pending", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Welcome()
        {
            UserViewModel a = new UserViewModel();
            var principal = await _userManager.GetUserAsync(User);
            a.Name = principal.FirstName;
            a.LastName = principal.FirstLastName;

            if (User.HasClaim("Permission", "admin"))
                return View(a);
            else
                RedirectToAction("Index", a);

            return View();
        }

        [HttpGet]
        public IActionResult Pending()
        {
            return View();
        }

        public IActionResult Provincia()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Provincia"));
        }

        public IActionResult Admin()
        {
            return RedirectToAction("FirstPage", "Admin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
