using BizData.Entities;
using BizDbAccess.Authentication;
using BizLogic.GenericInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.Authentication.Concrete
{
    public class RegisterUserAction : BizActionErrors, IBizAction<RegisterUsuarioCommand, Usuario>
    {
        private readonly UserDbAccess _dbAccess;

        public RegisterUserAction(UserDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }


        public async Task<IdentityResult> action(Usuario user, string Password)
        {
            var result = await _dbAccess.RegisterUsuarioAsync(user, Password);
            return HasErrors ? null : result;
        }

        public Usuario Action(RegisterUsuarioCommand dto)
        {
            throw new NotImplementedException();
        }
    }
}
