﻿using BizData.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfSeeder
    {
        private readonly EfCoreContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<Usuario> _userManager;

        public EfSeeder(EfCoreContext ctx, IHostingEnvironment hosting,
            UserManager<Usuario> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            //if (_userManager.FindByEmailAsync("raul@gmail.com").Result == null)
            //{
            //    var karl = new Usuario()
            //    {
            //        FirstName = "Karl",
            //        SecondName = "Lewis",
            //        FirstLastName = "Sosa",
            //        SecondLastName = "Justiz",
            //        Email = "klsj@gmail.com",
            //        UserName = "klsj@gmail.com"
            //    };

            //    var uo = new UnidadOrganizativa()
            //    {
            //        Nombre = "Plaza",
            //        Provincia = new Provincia()
            //        {
            //            Nombre = "La Habana"
            //        },
            //        Inversionistas = new List<Usuario>()
            //    };

            //    _userManager.CreateAsync(karl, "T3n!");
            //    _ctx.Add(uo);

            //    /*var filepath = Path.Combine(_hosting.ContentRootPath, "wwwroot/json/usuarios.json");
            //    var json = File.ReadAllText(filepath);
            //    var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);

            //    foreach (var user in usuarios)
            //    {
            //        _userManager.CreateAsync(user, "1234");
            //       // _userManager.AddClaimAsync(user, new Claim("Permission", "common"));
            //    }*/

            //    _ctx.SaveChanges();
            //    _userManager.AddClaimAsync(karl, new Claim("Permission", "admin"));
            //}
   
        }
    }
}
