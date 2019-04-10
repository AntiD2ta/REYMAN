using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizLogic.Authentication;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AccountServices
{
    public class LoginService
    {
        private readonly IUnitOfWork _context;
        private readonly UserDbAccess _dbAccess;

        public LoginService(IUnitOfWork context, SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _context = context;
            _dbAccess = new UserDbAccess(_context, signInManager, userManager);
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await _dbAccess.GetUserByEmailAsync(email);
        }

        public async Task<Usuario> EditUserAsync(Usuario entity, Usuario toUpd)
        {
            return await _dbAccess.UpdateAsync(entity, toUpd);
        }

        public async Task<Usuario> UpdateUsuarioUOAsync(Usuario toUpd, UnidadOrganizativa UO)
        {
            if (UO == null)
                throw new InvalidOperationException("La unidad organizativa no debe ser un objeto vacío.");

            toUpd = await _dbAccess.UpdateUOAsync(toUpd, UO);
            _context.Commit();
            return toUpd;
        }
    }
}
