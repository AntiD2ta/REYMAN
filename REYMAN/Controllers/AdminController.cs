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
using ServiceLayer.InvestorServices;
using BizLogic.Planning;
using ServiceLayer.AccountServices;
using BizLogic.Authentication;

namespace REYMAN.Controllers
{
    /// <summary>
    /// Manage all the views related to admin powers and actions.
    /// </summary>
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario>_signInManager;
        private readonly IUnitOfWork _context;
        private readonly GetterUtils _getterUtils;

        /// <summary>
        /// Constructor for the controller.
        /// </summary>
        /// <param name="userManager">Object of ASP.NET CORE Identity. Is the repository for Usuario entity</param>
        /// <param name="context">Unit of Work in charge of the access to the database. Configured
        /// in Startup/ConfigureServices</param>
        /// <param name="getterUtils">See description for this interface.</param>
        public AdminController(UserManager<Usuario> userManager,
            IUnitOfWork context,
            IGetterUtils getterUtils,
            SignInManager<Usuario>signInManager)
        {
            _userManager = userManager;
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
            _signInManager = signInManager;
        }

        /// <summary>
        /// GET method of FirstPage(sort of a home page)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FirstPage()
        {
            return View();
        }

        /// <summary>
        /// GET method of  EditPlanes view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditPlanes()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Plan"));
        }

        /// <summary>
        /// POST method of EditPlanes view.
        /// </summary>
        /// <param name="button">Type of the clicked button.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditPlanes(string button)
        {

            var action = button.Split("/");
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            var user = await _userManager.GetUserAsync(User);
            //pc.InmublesUO = user.UnidadOrganizativa.Inmuebles.Select(x=>x.Direccion);
            pc.InmublesUO = new List<string> ();
            if (action[0] == "Add")
                return RedirectToAction("AddPlan", "Admin");
            else
                pc.Set(((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID.ToString() == action[1]).Single());

            return RedirectToAction("PlanState", "Admin",pc);
        }

        public  IActionResult PlanState(PlanCommand cmd)
        {
          
            return View(cmd);
        }

        /// <summary>
        /// GET method of AddPlan view. This page manage the create Plan entity feature.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddPlan()
        {
            return View();
        }

        public IActionResult EditPlan(PlanCommand command)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            InvestorServices investorServices = new InvestorServices(_context);
            if (command.button == "Edit")
            {
                investorServices.UpdatePlan(command.ToPlan(), (((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID == command.PlanID).Single()));
                return RedirectToAction("EditPlanes", "Admin");
            }
            else
                return View(command);
        }

        /// <summary>
        /// POST method of AddPlan view.
        /// </summary>
        /// <param name="cmd">Data of new Plan.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPlan(PlanCommand cmd)
        {
            InvestorServices Is = new InvestorServices(_context);
            //display errors if errors is not null
            Is.RegisterPlan(cmd, out var errors);
            return RedirectToAction("EditPlanes", "Admin");
        }

        /// <summary>
        /// GET method of EditProvincia view. This page manage the edit/update Provincia feature.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditProvincia()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            ProvinciaViewModel pvm = new ProvinciaViewModel { GetProvincia = getter.GetAll("Provincia") };
            return View(pvm);
        }

        /// <summary>
        /// POST method of EditProvincia view.
        /// </summary>
        /// <param name="vm">Data of the new edited Provincia</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditProvincia(ProvinciaViewModel vm)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {
                AdminService ad = new AdminService(_context);
                if (vm.button == "Add")
                    ad.RegisterProvincia(vm);
                else
                    ad.DeleteProvincia((getter.GetAll("Provincia") as IEnumerable<Provincia>).Where(x => x.Nombre == vm.Nombre).Single());
                return RedirectToAction("EditProvincia", "Admin");
            }

            ModelState.AddModelError(string.Empty, "An error occured trying to edit the entity Provincia");

            //If we got to here, something went wrong
            vm.GetProvincia = getter.GetAll("Provincia");
            return View(vm);
        }
        [HttpGet]
        public IActionResult EditUOs()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("UnidadOrganizativa"));
        }
        public IActionResult PartialSelUO()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("UnidadOrganizativa"));
        }
        [HttpPost]
        public IActionResult EditUOs(string button)
        {
            var action = button.Split("/");
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            if (action[0] == "Add")
                return RedirectToAction("AddUO", "Admin");
            else
                pc.Set(((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID.ToString() == action[1]).Single());
            return RedirectToAction("EditUO", "Admin", pc);
        }
        [HttpGet]
        public IActionResult AddUO()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(new UOCommand { Provincias = getter.GetAll("Provincia") as IEnumerable<Provincia> });
        }
        [HttpPost]
        public IActionResult AddUO(UOCommand cmd)
        {
            AdminService adminService = new AdminService(_context);
            //display errors if errors is not null
            adminService.RegisterUO(cmd,out var errors);
            return RedirectToAction("EditUOs", "Admin");
        }
        [HttpGet]
        public IActionResult AddAccionCons()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(new AccionConsCommand { UnidadesMedida = new List<string> { "dollar","nacional" },  AccionConsts = new List<string> { "karl", "teno" } /*(getter.GetAll("AccionConstructiva") as IEnumerable<Provincia>).Select(x => x.Nombre) }*/});
        }
        [HttpPost]
        public IActionResult AddAccionCons(AccionConsCommand cmd)
        {
            InvestorServices investorServices = new InvestorServices(_context);
            investorServices.RegisterAccionCons(cmd,out var errors);
            return RedirectToAction("FirstPage", "Admin");
        }

        [HttpGet]
        public IActionResult AddInmueble()
        {
            return View(new InmuebleCommand() );
        }
        [HttpPost]
        public IActionResult AddInmueble(InmuebleCommand cmd)
        {
            InvestorServices investorServices = new InvestorServices(_context);
            //investorServices.RegisterInmueble(cmd,)
            return View(cmd);
        }
        public IActionResult Usuarios()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager,_userManager);
            return View(getter.GetAll("Usuario"));
        }
        
        public async Task<IActionResult> EditUsuario(RegisterUsuarioCommand cmd)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            GetterAll getter1 = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {
                LoginService loginService = new LoginService(_context, _signInManager, _userManager);
                var us = cmd.ToUsuario();
                us.UnidadOrganizativa= (getter1.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Where(x=>x.UnidadOrganizativaID==cmd.UO).Single();
                await loginService.EditUserAsync(us, (getter.GetAll("Usuario")as IEnumerable<Usuario>).Where(x=>x.Email==cmd.EditEmail).Single());
                return RedirectToAction("Usuarios", "Admin");
            }
            cmd.UOs = getter1.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>;
            return View(cmd);
        }
    }

    
}
