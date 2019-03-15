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

        public async Task<IdentityResult> UpdateAsync(Usuario entity, string email)
        {
            var user =  GetUserByEmailAsync(email).Result;
            if (user == null)
                throw new Exception("User to be updated no exist");

            //just for sure that the fields of the viewModel are not null
            //null-coalescing is used
            user.FirstName = entity.FirstName ?? user.FirstName; ;
            user.FirstLastName = entity.FirstLastName ?? user.FirstLastName;
            user.SecondName = entity.SecondName ?? user.SecondName;
            user.Email = entity.Email ?? user.Email;

            //this needs to be tested
            return await _userManager.UpdateAsync(user);
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
