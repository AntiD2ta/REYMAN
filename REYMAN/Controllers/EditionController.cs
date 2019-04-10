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

namespace REYMAN.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class EditionController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _context;
        private readonly GetterUtils _getterUtils;

        public EditionController(UserManager<Usuario> userManager,
            IUnitOfWork context,
            IGetterUtils getterUtils)
        {
            _userManager = userManager;
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
        }

        [HttpGet]
        public IActionResult FirstPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditPlanes()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Plan"));
        }
        public class A
        {
            public string button { get; set; }
        }
        [HttpPost]
        public IActionResult EditPlanes(A a)
        {
            var action = a.button.Split("/");
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            if (action[0] == "Add")
                return RedirectToAction("AddPlan", "Edition");
            else
                pc.Set(((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID.ToString() == action[1]).Single());
            return RedirectToAction("EditPlan", "Edition", pc);
        }


        public IActionResult EditPlan(PlanCommand command)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            InvestorServices investorServices = new InvestorServices(_context);
            if (command.button == "Edit")
            {
                investorServices.UpdatePlan(command.ToPlan(), (((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID == command.PlanID).Single()));
                return RedirectToAction("EditPlanes", "Edition");
            }
            else
                return View(command);
        }
        [HttpGet]
        public  IActionResult AddPlan()
        {
        //    var user=await _userManager.GetUserAsync(User);
        //    if (user.UnidadOrganizativa.Inmuebles)
            return View();
        }
        [HttpPost]
        public IActionResult AddPlan(PlanCommand cmd)
        {
            InvestorServices Is = new InvestorServices(_context);
            //display errors if errors is not null
            Is.RegisterPlan(cmd, out var errors);
            return RedirectToAction("EditPlanes", "Edition");
        }
        [HttpGet]
        public IActionResult EditProvincia()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            ProvinciaViewModel pvm = new ProvinciaViewModel { GetProvincia = getter.GetAll("Provincia") };
            return View(pvm);
        }

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
                return RedirectToAction("EditProvincia", "Edition");
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
        [HttpPost]
        public IActionResult EditUOs(A a)
        {
            var action = a.button.Split("/");
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            if (action[0] == "Add")
                return RedirectToAction("AddUO", "Edition");
            else
                pc.Set(((IEnumerable<Plan>)getter.GetAll("Plan")).Where(x => x.PlanID.ToString() == action[1]).Single());
            return RedirectToAction("EditPlan", "Edition", pc);
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
            adminService.RegisterUO(cmd, out var errors);
            return RedirectToAction("EditUOs", "Edition");
        }
    }

}
