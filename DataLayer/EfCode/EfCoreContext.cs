using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfCoreContext : IdentityDbContext<Usuario>, IUnitOfWork
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Plan> Planes { get; set; }
        public DbSet<UnidadOrganizativa> UnidadesOrganizativas { get; set; }
        public DbSet<AccionConstructiva> AccionesCons { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<ObjetoObra> ObjetosObra { get; set; }
        public DbSet<ManoObra> ManosObra { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

