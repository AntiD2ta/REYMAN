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
using System.Security.Claims;
using REYMAN.ViewModels;

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
        public AdminController(UserManager<Usuario> userManager,
            IUnitOfWork context,
            IGetterUtils getterUtils,
            SignInManager<Usuario> signInManager)
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
        public async Task<IActionResult> FirstPage()
        {
            AdminService _adminService = new AdminService(_context, _userManager, _getterUtils);
            FirstPageViewModel vm = new FirstPageViewModel();
            var t = await _adminService.FillNotificationsAsync();
            vm.UserPendings = t.UserPendings;
            vm.Provincias = t.Provincias;
            vm.UO = t.Provincias;

            return View(vm);
        }

        /// <summary>
        /// GET method of  EditPlanes view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Inversionista")]
        public async Task<IActionResult> EditPlanes()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return View(user.UnidadOrganizativa.Planes);
        }

        /// <summary>
        /// POST method of EditPlanes view.
        /// </summary>
        /// <param name="button">Type of the clicked button.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditPlanes(string button)
        {
            var action = button.Split("/");
            if (action[0] == "Add")
                return RedirectToAction("AddPlan", "Admin");
            else
                return RedirectToAction("PlanState", new PlanCommand() { PlanID = int.Parse(action[1]) });
        }

        [HttpGet]
        [Authorize("Inversionista")]
        public async Task<IActionResult> PlanState(PlanCommand pc)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Plan plan = new Plan();

            foreach (var pl in user.UnidadOrganizativa.Planes)
            {
                if (pc.PlanID == pl.PlanID)
                {
                    plan = pl;
                    break;
                }
            }
            return View(plan);
        }

        [HttpPost]
        public IActionResult PlanState(string button)
        {
            var action = button.Split("/");
            if (action[0] == "Add")
                return RedirectToAction("AddAccionCons", new AccionConsCommand() { PlanID = int.Parse(action[1]) });
            else
                return RedirectToAction("Materiales_AccCons", new AccionConsCommand() { AccionConstructivaID = int.Parse(button) });
        }

        [HttpGet]
        [Authorize("Inversionista")]
        public async Task<IActionResult> Materiales_AccCons(AccionConsCommand pc)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var acc = new AccionConstructiva();

            foreach (var pl in user.UnidadOrganizativa.Planes)
            {
                foreach (var ac in pl.AccionesConstructivas)
                {
                    if (pc.AccionConstructivaID == ac.AccionConstructivaID)
                    {
                        acc = ac;
                        break;
                    }
                }
            }
            return View(acc);
        }

        /// <summary>
        /// GET method of AddPlan view. This page manage the create Plan entity feature.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Inversionista")]
        public IActionResult AddPlan()
        {
            return View();
        }

        [Authorize("Inversionista")]
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
        public async Task<IActionResult> AddPlan(PlanCommand cmd)
        {
            InvestorServices Is = new InvestorServices(_context);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            foreach (var ud in (new GetterAll(_getterUtils, _context).GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>))
            {
                if (ud.Nombre == user.UnidadOrganizativa.Nombre)
                {
                    cmd.UO = ud;
                    break;
                }
            }

            Is.RegisterPlan(cmd, out var errors);
            return RedirectToAction("EditPlanes", "Admin");
        }

        /// <summary>
        /// GET method of EditProvincia view. This page manage the edit/update Provincia feature.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditProvincias()
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
        [Authorize("Admin")]
        public IActionResult EditProvincias(ProvinciaViewModel vm)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {
                AdminService ad = new AdminService(_context, _userManager, _getterUtils);
                if (vm.button == "Add")
                    ad.RegisterProvincia(vm);
                else
                    ad.DeleteProvincia((getter.GetAll("Provincia") as IEnumerable<Provincia>).Where(x => x.Nombre == vm.Nombre).Single());
                return RedirectToAction("EditProvincias", "Admin");
            }

            ModelState.AddModelError(string.Empty, "An error occured trying to edit the entity Provincia");

            //If we got to here, something went wrong
            vm.GetProvincia = getter.GetAll("Provincia");
            return View(vm);
        }

        [Authorize("Admin")]
        public IActionResult EditProvincia(ProvinciaViewModel pvm)
        {
            if (ModelState.IsValid && pvm.button == "sub")
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                AdminService ad = new AdminService(_context, _userManager, _getterUtils);
                var prov = (getter.GetAll("Provincia") as IEnumerable<Provincia>).Where(x => x.ProvinciaID == pvm.Id).Single();
                ad.UpdateProvincia(pvm.ToProvincia(), prov);
                return RedirectToAction("EditProvincias", "Admin");
            }
            return View(pvm);
        }

        [HttpGet]
        [Authorize("Inversionista")]
        public async Task<IActionResult> EditAC(int ac_id)
        {
            //TODO: [Teno] te mande la accion constructiva xq no tengo idea de q quieras. Entras a la vista y lo cambias a conveniencia x el id quizas
            var vm = new EditACViewModel();
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var inmueble = (await _userManager.FindByEmailAsync(User.Identity.Name)).UnidadOrganizativa.Inmuebles;
            vm.Inmuebles = inmueble;
            vm.Unidades = (getter.GetAll("UnidadMedida") as IEnumerable<UnidadMedida>);
            vm.Especialidades = (getter.GetAll("Especialidad") as IEnumerable<Especialidad>);

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditAC(EditACViewModel vm)
        {
            //TODO: [Teno] con el Id puedes redirigir nuevamente al plan         

            return View(vm);
        }

        [HttpGet]
        [Authorize("Inversionista")]
        public async Task<IActionResult> AddAcMaterial(int ac_id)
        {
            //TODO: [Teno] te mande la accion constructiva xq no tengo idea de q quieras. Entras a la vista y lo cambias a conveniencia x el id quizas
            var vm = new EditACViewModel();
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var inmueble = (await _userManager.FindByEmailAsync(User.Identity.Name)).UnidadOrganizativa.Inmuebles;
            vm.Inmuebles = inmueble;
            vm.Unidades = (getter.GetAll("UnidadMedida") as IEnumerable<UnidadMedida>);
            vm.Especialidades = (getter.GetAll("Especialidad") as IEnumerable<Especialidad>);

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddAcMaterial(EditACViewModel vm)
        {
            //TODO: [Teno] con el Id puedes redirigir nuevamente al plan         

            return View(vm);
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<IActionResult> EditUOs()
        {
            if (User.HasClaim("Permission", "admin"))
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                return View(getter.GetAll("UnidadOrganizativa"));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                return View(new List<UnidadOrganizativa>() { user.UnidadOrganizativa });
            }
        }

        public async Task<IActionResult> PartialSelUO()
        {
            if (User.HasClaim("Permission", "admin"))
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                return View(getter.GetAll("UnidadOrganizativa"));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                return View(new List<UnidadOrganizativa>() { user.UnidadOrganizativa });
            }
        }

        [HttpPost]
        public IActionResult EditUOs(string button)
        {
            AdminService adminService = new AdminService(_context, _userManager, _getterUtils);
            var action = button.Split("/");
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            if (action[0] == "Add")
                return RedirectToAction("AddUO", "Admin");
            else if (action[0] == "Delete")
                adminService.DeleteUO((getter.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>)
                    .Where(x => x.UnidadOrganizativaID.ToString() == action[1]).Single());
            return RedirectToAction("EditUOs", "Admin", pc);
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult AddUO()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            return View(new UOCommand { Provincias = getter.GetAll("Provincia") as IEnumerable<Provincia> });
        }

        [HttpPost]
        public IActionResult AddUO(UOCommand cmd)
        {
            AdminService adminService = new AdminService(_context, _userManager, _getterUtils);
            //display errors if errors is not null
            adminService.RegisterUO(cmd, out var errors);
            return RedirectToAction("EditUOs", "Admin");
        }

        [Authorize("Admin")]
        public IActionResult EditUO(UOViewModel uovm)
        {
            if (ModelState.IsValid && uovm.button == "sub")
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                AdminService ad = new AdminService(_context, _userManager, _getterUtils);

                var prov = (getter.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Where(x => x.UnidadOrganizativaID == uovm.Id).Single();
                ad.UpdateUO(uovm.ToUO(), prov);
                return RedirectToAction("EditUOs", "Admin");
            }
            return View(uovm);
        }

        [Authorize("Inversionista")]
        public async Task<IActionResult> AddAccionCons(AccionConsCommand cmd)
        {
            if (ModelState.IsValid)
            {
                InvestorServices investorServices = new InvestorServices(_context);
                investorServices.RegisterAccionCons(cmd, out var errors);
                return RedirectToAction("EditPlanes", "Admin");
            }

            GetterAll getter = new GetterAll(_getterUtils, _context);
            var inmueble = (await _userManager.FindByEmailAsync(User.Identity.Name)).UnidadOrganizativa.Inmuebles;
            cmd.Inmuebles = inmueble;
            cmd.UnidadesMedida = (getter.GetAll("UnidadMedida") as IEnumerable<UnidadMedida>).Select(um => um.Nombre);
            cmd.Especialidades = (getter.GetAll("Especialidad") as IEnumerable<Especialidad>);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            IEnumerable<AccionConstructiva> ac = new List<AccionConstructiva>();

            foreach (var inm in user.UnidadOrganizativa.Inmuebles)
            {
                foreach (var obj in inm.ObjetosDeObra)
                {
                    ac = ac.Concat(obj.AccionesConstructivas);
                }
            }
            cmd.AccionConsts = ac.Select(act => act.Nombre);
            return View(cmd);
        }

        [HttpGet]
        public async Task<IActionResult> EditInmuebles()
        {
            if (User.HasClaim("Permission", "admin"))
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                var inmuebles = (getter.GetAll("Inmueble") as IEnumerable<Inmueble>);
                return View(inmuebles);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                return View(user.UnidadOrganizativa.Inmuebles);
            }
        }

        [HttpPost]
        public IActionResult EditInmuebles(int id)
        {
            InvestorServices adminService = new InvestorServices(_context);
             GetterAll getter = new GetterAll(_getterUtils, _context);
            adminService.DeleteInmueble((getter.GetAll("Inmueble") as IEnumerable<Inmueble>)
                .Where(x => x.InmuebleID == id).Single());
            return RedirectToAction("EditInmuebles", "Admin");
        }

        public IActionResult EditInmueble(InmuebleCommand cmd)
        {
            if (ModelState.IsValid && cmd.button == "sub")
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                InvestorServices ins = new InvestorServices(_context);

                var inm = (getter.GetAll("Inmueble") as IEnumerable<Inmueble>).Where(x => x.InmuebleID == cmd.Id).Single();
                cmd.UO = inm.UnidadOrganizativa;
                try
                {
                    ins.UpdateInmueble(cmd.ToInmueble(), inm);
                    return RedirectToAction("EditInmuebles", "Admin");
                }
                catch (InvalidOperationException e)
                {
                    cmd.error = e.Message;
                }

            }
            return View(cmd);
        }

        [HttpGet]
        public IActionResult AddInmueble()
        {
            return View(new InmuebleCommand());
        }

        [HttpPost]
        public async Task<IActionResult> AddInmueble(InmuebleCommand cmd)
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                InvestorServices investorServices = new InvestorServices(_context);
                investorServices.RegisterInmueble(cmd, user.UnidadOrganizativa.Nombre, out var errors);
                return RedirectToAction("EditInmuebles", "Admin");
            }
            return View(cmd);
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult AddInmuebleAdm()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context);
            ViewData["UO"] = getter.GetAll("UnidadOrganizativa");
            return View(new InmuebleCommand());
        }

        [HttpPost]
        public async Task<IActionResult> AddInmuebleAdm(InmuebleCommand cmd)
        {
         
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                InvestorServices investorServices = new InvestorServices(_context);
                investorServices.RegisterInmueble(cmd, cmd.UOnombre, out var errors);
                return RedirectToAction("EditInmuebles", "Admin");
            }
            return View(cmd);
        }

        [HttpGet]
        public async Task<IActionResult> AddObjObra()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var inmuebles = user.UnidadOrganizativa.Inmuebles.Select(inm => inm.Direccion);
            return View(new ObjObraCommand { Inmuebles = inmuebles });
        }

        [HttpPost]
        public async Task<IActionResult> AddObjObra(ObjObraCommand cmd)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                InvestorServices investorServices = new InvestorServices(_context);
                cmd.nombreUO = user.UnidadOrganizativa.Nombre;
                investorServices.RegisterObjObra(cmd, cmd.Direccion, out var errors);
                return RedirectToAction("EditObjObras", "Admin");
            }
            return View(cmd);
        }

        [HttpGet]
        public async Task<IActionResult> EditObjObras()
        {
            if (User.HasClaim("Permission", "admin"))
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                return View(getter.GetAll("ObjetoObra"));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                IEnumerable<ObjetoObra> obs = new List<ObjetoObra>();

                foreach (var inm in user.UnidadOrganizativa.Inmuebles)
                    obs = obs.Concat(inm.ObjetosDeObra);

                return View(obs);
            }
        }

        [HttpPost]
        public IActionResult EditObjObras(string button)
        {
            AdminService adminService = new AdminService(_context, _userManager, _getterUtils);
            var action = button.Split("/");
            InvestorServices investorServices = new InvestorServices(_context);
            GetterAll getter = new GetterAll(_getterUtils, _context);
            var pc = new PlanCommand();
            if (action[0] == "Add")
                return RedirectToAction("AddObjObra", "Admin");
            else if (action[0] == "Delete")
                investorServices.DeleteObjetoObra((getter.GetAll("ObjetoObra") as IEnumerable<ObjetoObra>)
                     .Where(x => x.ObjetoObraID.ToString() == action[1]).Single());
            return EditObjObras().Result;
        }

        public IActionResult EditObjObra(ObjObraCommand uocmd)
        {
            if (ModelState.IsValid && uocmd.button == "sub")
            {
                GetterAll getter = new GetterAll(_getterUtils, _context);
                InvestorServices ins = new InvestorServices(_context);

                var prov = (getter.GetAll("ObjetoObra") as IEnumerable<ObjetoObra>).Where(x => x.ObjetoObraID == uocmd.Id).Single();
                uocmd.Inmueble = prov.Inmueble;
                ins.UpdateObjetoObra(uocmd.ToObjObra(), prov);
                return RedirectToAction("EditObjObras", "Admin");
            }
            return View(uocmd);
        }

        [Authorize("Admin")]
        public IActionResult Usuarios()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            return View(getter.GetAll("Usuario"));
        }

        [Authorize("Admin")]
        public async Task<IActionResult> EditUsuario(RegisterUsuarioCommand cmd)
        {

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            GetterAll getter1 = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(cmd.Email);
                var claims = (await _userManager.GetClaimsAsync(user)).ToList();
                var permission = claims.Where(x => x.Type == "Permission").SingleOrDefault();
                if (permission == null) 
                   await _userManager.AddClaimAsync(user, new Claim("Permission", cmd.Claim));
                else if (permission.Value != cmd.Claim)
                {
                    await _userManager.RemoveClaimAsync(user, permission);
                    await _userManager.AddClaimAsync(user, new Claim("Permission", cmd.Claim));
                }
                LoginService loginService = new LoginService(_context, _signInManager, _userManager);
                var us = cmd.ToUsuario();
                us.UnidadOrganizativa = (getter1.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>).Where(x => x.UnidadOrganizativaID == cmd.UO).Single();
                await loginService.EditUserAsync(us, (getter.GetAll("Usuario") as IEnumerable<Usuario>).Where(x => x.Email == cmd.EditEmail).Single());
                return RedirectToAction("Usuarios", "Admin");
            }
            cmd.UOs = getter1.GetAll("UnidadOrganizativa") as IEnumerable<UnidadOrganizativa>;
            return View(cmd);
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<IActionResult> PendingUsers()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            List<Usuario> pending = new List<Usuario>();
            foreach (var item in getter.GetAll("Usuario"))
            {
                if ((await _userManager.GetClaimsAsync(item as Usuario)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    pending.Add(item as Usuario);
            }

            PendingUsersViewModel vm = new PendingUsersViewModel()
            {
                Usuarios = pending,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PendingUsers(PendingUsersViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.userID);
            await _userManager.RemoveClaimAsync(user, new Claim("Pending", "true"));
            await _userManager.AddClaimAsync(user, new Claim("Pending", "false"));
            await _userManager.AddClaimAsync(user, new Claim("Permission", "inversionista"));
            _context.Commit();

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            List<Usuario> pending = new List<Usuario>();
            foreach (var item in getter.GetAll("Usuario"))
            {
                if ((await _userManager.GetClaimsAsync(item as Usuario)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    pending.Add(item as Usuario);
            }
            return RedirectToAction("PendingUsers", "Admin");
        }

        [HttpGet]
        public IActionResult AddMaterial()
        {
            var lvm = new MaterialViewModel();
            lvm.UnidadesMedida = (new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>);
            return View(lvm);
        }

        [HttpPost]
        public IActionResult AddMaterial(MaterialViewModel lvm)
        {
            var unidadMedida = (new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>).Where(um => um.Nombre == lvm.UnidadMedida).Single();
            var service = new InvestorServices(_context);
            service.RegisterMaterial(new MaterialCommand(null, new Material() { Nombre = lvm.Nombre, UnidadMedida = unidadMedida }), out var errors);
            return RedirectToAction("EditMateriales");
        }


        public IActionResult EditMaterial(MaterialViewModel lvm)
        {
            lvm.UnidadesMedida = new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>;
            if (ModelState.IsValid && lvm.Button == "post")
            {
                new InvestorServices(_context).UpdateMaterial(new Material() { Nombre = lvm.Nombre, UnidadMedida = (new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>).Where(um => um.Nombre == lvm.UnidadMedida).Single() },
                                                              new AccionC_Material() { Material = (new GetterAll(_getterUtils, _context).GetAll("Material") as IEnumerable<Material>).Where(mat => mat.MaterialID == lvm.MaterialId).Single() },
                                                              out var errors);

                return RedirectToAction("EditMateriales");
            }
            return View(lvm);
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult EditMateriales()
        {
            var materiales = new GetterAll(_getterUtils, _context).GetAll("Material") as IEnumerable<Material>;
            ViewData["Materiales"] = materiales;
            return View(false);
        }

        [HttpPost]
        public IActionResult EditMateriales(string button)
        {
            var action = button.Split('/');
            if (action[0] == "Add")
                return RedirectToAction("AddMaterial");
            else if (action[0] == "Delete")
            {
                try
                {
                    new InvestorServices(_context).DeleteMaterial((new GetterAll(_getterUtils, _context).GetAll("Material") as IEnumerable<Material>).Where(mat => mat.MaterialID == int.Parse(action[1])).Single());
                    return RedirectToAction("EditMateriales");
                }
                catch
                {
                    var materiales = new GetterAll(_getterUtils, _context).GetAll("Material") as IEnumerable<Material>;
                    ViewData["Materiales"] = materiales;
                    return View(true);
                }
            }
            else
            {
                var material = (new GetterAll(_getterUtils, _context).GetAll("Material") as IEnumerable<Material>).Where(mat => mat.MaterialID == int.Parse(action[1])).Single();
                return RedirectToAction("EditMaterial", new MaterialViewModel()
                {
                    Nombre = material.Nombre,
                    MaterialId = material.MaterialID,
                    UnidadMedida = material.UnidadMedida.Nombre
                });
            }
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult AddUnidadMedida()
        {
            return View(new UnidadMedidaCommand());
        }

        [HttpPost]
        public IActionResult AddUnidadMedida(UnidadMedidaCommand cmd)
        {
            if (ModelState.IsValid)
                new InvestorServices(_context).RegisterUnidadMedida(cmd, out var errors);
            return RedirectToAction("EditUnidadesMedida");
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult EditUnidadesMedida()
        {
            var ums = new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>;
            return View(new UMViewModel { UnidadMedida = ums });
        }

        [HttpPost]
        public IActionResult EditUnidadesMedida(UMViewModel vm)
        {

            var temp = (new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>).Where(um => um.UnidadMedidaID == vm.id).Single();
            try
            {
                new InvestorServices(_context).DeleteUnidadMedida(temp);
                return RedirectToAction("EditUnidadesMedida");
            }
            catch
            {
                var ums = new GetterAll(_getterUtils, _context).GetAll("UnidadMedida") as IEnumerable<UnidadMedida>;
                vm.UnidadMedida = ums;
                ViewData["Nombre"] = temp.Nombre;
                return View(vm);
                //TODO: [Karle] corregir el error para que diga: Esa unidad de medida no puede ser eliminada porque existe un material o una accion constructiva que la esta usando.
            }
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult AddEspecialidad()
        {
            return View(new EspecialidadCommand());
        }

        [HttpPost]
        public IActionResult AddEspecialidad(EspecialidadCommand cmd)
        {
            if (ModelState.IsValid)
                new InvestorServices(_context).RegisterEspecialidad(cmd, out var errors);
            return RedirectToAction("EditEspecialidades");
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult EditEspecialidades()
        {
            var esp = new GetterAll(_getterUtils, _context).GetAll("Especialidad") as IEnumerable<Especialidad>;
            return View(new EspViewModel { Especialidad = esp });
        }

        [HttpPost]
        public IActionResult EditEspecialidades(EspViewModel vm)
        {

            var es = new GetterAll(_getterUtils, _context).GetAll("Especialidad") as IEnumerable<Especialidad>;
            var temp = es.Where(esp => esp.EspecialidadID == vm.id).Single();
            try
            {
                new InvestorServices(_context).DeleteEspecialidad(temp);
                return RedirectToAction("EditEspecialidades");
            }
            catch
            {
                ViewData["Nombre"] = temp.Tipo;
                vm.Especialidad = es;
                return View(vm);
                //TODO: [Karle] corregir el error para q diga: Esa Especialidad no puede ser eliminada porque existe una accion constructiva que la esta usando.
            }


        }

        [Authorize("Inversionista")]
        public IActionResult EditAccionCons_Mat(AccionCons_MaterialViewModel vm)
        {
            if (ModelState.IsValid && vm.Button == "post")
            {
                new InvestorServices(_context).UpdateACM(new AccionC_Material()
                {
                    Cantidad = vm.Cantidad,
                    PrecioCUC = vm.PrecioCUC,
                    PrecioCUP = vm.PrecioCUP
                },
                                                            (new GetterAll(_getterUtils, _context).GetAll("AccionC_Material") as IEnumerable<AccionC_Material>).Where(acm => acm.AccionC_MaterialID == vm.AccionCons_MaterialID).Single());

                return RedirectToAction("Materiales_AccCons", new AccionConsCommand() { AccionConstructivaID = vm.AccionConstructivaID });
            }
            return View(vm);
        }
    }
}
