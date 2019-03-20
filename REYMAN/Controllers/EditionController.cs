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
    public class EditionController:Controller
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
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Provincia"));
        }
        [HttpPost]
        public IActionResult EditProvincia(ProvinciaViewModel vm)
        {
            AdminService ad = new AdminService(_context);
            if (vm.button == "Add")
                ad.RegisterProvincia(vm);
            else 
                ad.DeleteProvincia(ad.GetProvincias().Where(x=>x.Nombre==vm.NombreBorrar).ToList()[0]);
            return RedirectToAction("EditProvincia", "Edition");
        }
        [HttpGet]
        public IActionResult EditProvincia()
        {
            AdminService ad = new AdminService(_context);
            return View(ad.GetProvincias());
        }
    }
    
}
