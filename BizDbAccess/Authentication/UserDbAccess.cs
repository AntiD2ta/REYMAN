using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDbAccess.Authentication
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore. This repository targets 
    /// EntityFrameworkCore necessarily, since SignInManager and UserManager classes are configured
    /// with EntityFrameworkCore.
    /// </summary>
    public class UserDbAccess : IEntityDbAccess<Usuario>
    {
        private readonly EfCoreContext _context;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UserDbAccess(IUnitOfWork context, SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _context = (EfCoreContext)context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUsuarioAsync(Usuario user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public void Add(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<Usuario> UpdateAsync(Usuario entity, Usuario user)
        {
            if (user == null)
                throw new Exception("User to be updated no exist");

            //just for sure that the fields of the viewModel are not null
            //null-coalescing is used
            user.FirstName = entity.FirstName ?? user.FirstName; ;
            user.SecondName = entity.SecondName ?? user.SecondName;
            user.FirstLastName = entity.FirstLastName ?? user.FirstLastName;
            user.SecondLastName = entity.SecondLastName ?? user.SecondLastName;

            await _userManager.UpdateAsync(user);
            return user;
        }

        public Usuario Update(Usuario entity, Usuario toUpd)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> UpdateUOAsync(Usuario toUpd, UnidadOrganizativa UO)
        {
            toUpd.UnidadOrganizativa = UO;
            await _userManager.UpdateAsync(toUpd);
            return toUpd;
        }
    }
}
