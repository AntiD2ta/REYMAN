using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizLogic.Authentication;
using BizLogic.Authentication.Concrete;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AccountServices
{
    public class RegisterService
    {
        private readonly RegisterUserAction _runner;

        public RegisterService(IUnitOfWork context, SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _runner = new RegisterUserAction(new UserDbAccess(context, signInManager, userManager));
        }

        public async Task<IdentityResult> RegisterUsuarioAsync(Usuario user, string password)
        {
            var result = await _runner.action(user, password);
           
            if (_runner.HasErrors) return null;

            return result;
        }
    }
}
