using BizDbAccess.GenericInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BizLogic.Authentication;
using ServiceLayer.AccountServices;
using BizData.Entities;
using ServiceLayer.Reports;
using System.Collections.Generic;
using ServiceLayer.SendMessage;
using ServiceLayer.AdminServices;
using BizLogic.Reports;
using DataLayer.EfCode;

namespace REYMAN.Controllers
{
    /// <summary>
    /// Manage all the views related to authentication
    /// </summary>
    [Authorize("LevelOneAuth")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor for the controller.
        /// </summary>
        /// <param name="context">Unit of Work in charge of the access to the database. Configured
        /// in Startup/ConfigureServices</param>
        /// <param name="signInManager">Object of ASP.NET CORE Identity, in charge of make effective
        /// the sign in and sign out of a user.</param>
        /// <param name="userManager">Object of ASP.NET CORE Identity. Is the repository for Usuario entity</param>
        /// <param name="config">Is used for the bearer token in CreateToken method.</param>
        public AccountController(IUnitOfWork context,
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IConfiguration config)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// GET (Async)method for Edit account page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var cmd = new RegisterUsuarioCommand();
            cmd.SetViewModel(user);
            return View(cmd);
        }

        /// <summary>
        /// POST (Async)method for Edit account page.
        /// </summary>
        /// <param name="cmd">cmd is an object containing the view model
        /// with the data of the new edited account.</param>
        /// <returns></returns>ee
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(RegisterUsuarioCommand cmd)
        {
            if (ModelState.IsValid)
            {
                var log = new LoginService(_context, _signInManager, _userManager);
                var userToUpd = await _userManager.GetUserAsync(User);
                var user = cmd.ToUsuario();

                user = await log.EditUserAsync(user, userToUpd);
                await _signInManager.RefreshSignInAsync(user);

                //We need to create UserViewModel

                //var uvm = new UserViewModel();
                //uvm.SetProperties(cmd);
                //uvm.Email = User.Identity.Name;
                //uvm.SetPermissions(User.Claims);

                //if (Request.Query.Keys.Contains("ReturnUrl"))
                //{
                //    return Redirect(Request.Query["ReturnUrl"].First());
                //}
                //else
                //{
                //    return RedirectToAction("Welcome", "User", uvm);
                //}

            }

            ModelState.AddModelError(string.Empty, "An error occured trying to register the user");

            //If we got to here, something went wrong
            return View(cmd);
        }

        /// <summary>
        /// GET method for Register account page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterUsuarioCommand());
        }
        
        /// <summary>
        /// POST (Async)method for Register account page. If the user can register succefully, then a claim
        /// Pending: true is assigned to it, as this user needs to be aproved by an admin to complete his
        /// registration to the system.
        /// </summary>
        /// <param name="cmd">cmd is a object containing the view model
        /// with the data of the new account.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUsuarioCommand cmd)
        {  
            if (ModelState.IsValid)
            {
                var _registerService = new RegisterService(_context, _signInManager, _userManager);
                var user = cmd.ToUsuario();
                var result = await _registerService.RegisterUsuarioAsync(user, cmd.Password);

                if (result.Succeeded)
                {   
                    var claim = new Claim("Pending", "true");
                    await _userManager.AddClaimAsync(user, claim);

                    await _signInManager.SignInAsync(user, false);

                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        //new SendMessage().Send(cmd.FirstName, cmd.SecondName, cmd.FirstLastName, cmd.SecondLastName, cmd.Email);
                        return RedirectToAction("Pending", "Home");
                    }
                }
                AddErrors(result);
            }

            ModelState.AddModelError(string.Empty, "An error occured trying to register the user");
            
            //If we got to here, something went wrong
            return View(cmd);
        }

        /// <summary>
        /// GET method for Sign in account page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        /// <summary>
        /// POST (Async)method for Sign in account page.
        /// </summary>
        /// <param name="lvm">View Model with am username and password.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email,
                                                                lvm.Password,
                                                                lvm.RememberMe,
                                                                false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Su Email o su Contraseña es incorrecta.");

            //If we got to here, something went wrong
            return View(lvm);
        }
        
        /// <summary>
        /// GET (async)method in charge of sign out an user of the system.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");           
        }

        /// <summary>
        /// (Async)Method that creates a Bearer Token for security.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        // Create the token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };
                        
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Helper method to add errors to be displayed.
        /// </summary>
        /// <param name="result"></param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

    }
}
