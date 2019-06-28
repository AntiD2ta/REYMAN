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
        public DbSet<AccionC_Material> AccCons_Mat { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Provincia>()
                .HasMany(prov => prov.UnidadesOrganizativas)
                .WithOne(ud => ud.Provincia)
                .IsRequired();

            builder.Entity<UnidadOrganizativa>()
                .HasMany(ud => ud.Inmuebles)
                .WithOne(inm => inm.UnidadOrganizativa)
                .IsRequired();

            builder.Entity<UnidadOrganizativa>()
                .HasMany(ud => ud.Planes)
                .WithOne(plan => plan.UnidadOrganizativa)
                .IsRequired();                

            builder.Entity<UnidadOrganizativa>()
                .HasMany(ud => ud.Usuarios)
                .WithOne(user => user.UnidadOrganizativa)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Inmueble>()
                .HasMany(ud => ud.ObjetosDeObra)
                .WithOne(obj => obj.Inmueble)
                .IsRequired();

            builder.Entity<ObjetoObra>()
                .HasMany(obj => obj.AccionesConstructivas)
                .WithOne(ac => ac.ObjetoObra)
                .IsRequired();

            builder.Entity<Plan>()
               .HasMany(plan => plan.AccionesConstructivas)
               .WithOne(ac => ac.Plan)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AccionC_Material>()
                .HasOne(acm => acm.AccionConstructiva)
                .WithMany(ac => ac.Materiales)
                .IsRequired();

            builder.Entity<AccionC_Material>()
                .HasOne(acm => acm.Material)
                .WithMany(mat => mat.AccionesConstructivas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

