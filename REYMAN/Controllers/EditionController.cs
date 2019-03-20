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
    public class EditionController:Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _context;
        public EditionController(UserManager<Usuario> userManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult FirstPage()
        {
            AdminService ad = new AdminService(_context);
            return View(ad.GetProvincias());
        }
        [HttpPost]
        public IActionResult EditProvincia(ProvinciaViewModel vm)
        {
            AdminService ad = new AdminService(_context);
            if (a.button == "Add")
                ad.RegisterProvincia(a);
            else 
                ad.DeleteProvincia(ad.GetProvincias().Where(x=>x.Nombre==a.NombreBorrar).ToList()[0]);
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
