﻿// <auto-generated />
using System;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(EfCoreContext))]
    [Migration("20190310231756_Identity Schema")]
    partial class IdentitySchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizData.Entities.Ciudad", b =>
                {
                    b.Property<int>("CiudadID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.Property<string>("PaisID");

                    b.Property<int?>("ViajeID");

                    b.HasKey("CiudadID");

                    b.HasIndex("PaisID");

                    b.HasIndex("ViajeID");

                    b.ToTable("Ciudades");
                });

            modelBuilder.Entity("BizData.Entities.Institucion", b =>
                {
                    b.Property<int>("InstitucionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.Property<int?>("ViajeID");

                    b.HasKey("InstitucionID");

                    b.HasIndex("ViajeID");

                    b.ToTable("Instituciones");
                });

            modelBuilder.Entity("BizData.Entities.Pais", b =>
                {
                    b.Property<string>("PaisID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ViajeID");

                    b.HasKey("PaisID");

                    b.HasIndex("ViajeID");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("BizData.Entities.Pais_Visa", b =>
                {
                    b.Property<int>("Pais_VisaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaisID");

                    b.Property<int?>("ViajeID");

                    b.Property<int?>("VisaID");

                    b.HasKey("Pais_VisaID");

                    b.HasIndex("PaisID");

                    b.HasIndex("ViajeID");

                    b.HasIndex("VisaID");

                    b.ToTable("Pais_Visa");
                });

            modelBuilder.Entity("BizData.Entities.Pasaporte", b =>
                {
                    b.Property<int>("PasaporteID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Actualizaciones");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<int>("Tipo");

                    b.Property<long>("UsuarioCI");

                    b.Property<string>("UsuarioId");

                    b.HasKey("PasaporteID");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pasaportes");
                });

            modelBuilder.Entity("BizData.Entities.Pasaporte_Visa", b =>
                {
                    b.Property<int>("Pasaporte_VisaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PasaporteID");

                    b.Property<int?>("VisaID");

                    b.HasKey("Pasaporte_VisaID");

                    b.HasIndex("PasaporteID");

                    b.HasIndex("VisaID");

                    b.ToTable("Pasaporte_Visa");
                });

            modelBuilder.Entity("BizData.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstLastName");

                    b.Property<string>("FirstName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondLastName");

                    b.Property<string>("SecondName");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BizData.Entities.Viaje", b =>
                {
                    b.Property<int>("ViajeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Costo");

                    b.Property<DateTime>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<int>("MotivoViaje");

                    b.Property<string>("UsuarioId");

                    b.HasKey("ViajeID");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Viajes");
                });

            modelBuilder.Entity("BizData.Entities.Visa", b =>
                {
                    b.Property<int>("VisaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("VisaID");

                    b.ToTable("Visa");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BizData.Entities.Ciudad", b =>
                {
                    b.HasOne("BizData.Entities.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID");

                    b.HasOne("BizData.Entities.Viaje")
                        .WithMany("Ciudades")
                        .HasForeignKey("ViajeID");
                });

            modelBuilder.Entity("BizData.Entities.Institucion", b =>
                {
                    b.HasOne("BizData.Entities.Viaje")
                        .WithMany("Instituciones")
                        .HasForeignKey("ViajeID");
                });

            modelBuilder.Entity("BizData.Entities.Pais", b =>
                {
                    b.HasOne("BizData.Entities.Viaje")
                        .WithMany("Pais")
                        .HasForeignKey("ViajeID");
                });

            modelBuilder.Entity("BizData.Entities.Pais_Visa", b =>
                {
                    b.HasOne("BizData.Entities.Pais", "Pais")
                        .WithMany("Visas")
                        .HasForeignKey("PaisID");

                    b.HasOne("BizData.Entities.Viaje", "Viaje")
                        .WithMany()
                        .HasForeignKey("ViajeID");

                    b.HasOne("BizData.Entities.Visa")
                        .WithMany("Paises")
                        .HasForeignKey("VisaID");
                });

            modelBuilder.Entity("BizData.Entities.Pasaporte", b =>
                {
                    b.HasOne("BizData.Entities.Usuario", "Usuario")
                        .WithMany("Pasaportes")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("BizData.Entities.Pasaporte_Visa", b =>
                {
                    b.HasOne("BizData.Entities.Pasaporte", "Pasaporte")
                        .WithMany("Visas")
                        .HasForeignKey("PasaporteID");

                    b.HasOne("BizData.Entities.Visa", "Visa")
                        .WithMany("Pasaportes")
                        .HasForeignKey("VisaID");
                });

            modelBuilder.Entity("BizData.Entities.Viaje", b =>
                {
                    b.HasOne("BizData.Entities.Usuario", "Usuario")
                        .WithMany("Viajes")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BizData.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BizData.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BizData.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BizData.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
