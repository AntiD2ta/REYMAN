using BizData.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            if (_userManager.FindByEmailAsync("klsj@gmail.com").Result == null)
            {
                var karl = new Usuario()
                {
                    FirstName = "Karl",
                    SecondName = "Lewis",
                    FirstLastName = "Sosa",
                    SecondLastName = "Justiz",
                    Email = "klsj@gmail.com",
                    UserName = "klsj@gmail.com"
                };
                
                await _userManager.CreateAsync(karl, "T3n!");
                var claim = new Claim("Permission", "admin");
                await _userManager.AddClaimAsync(karl, claim);

                #region PROVINCIAS
                var prov1 = new Provincia()
                {
                    Nombre = "Pinar del Rio",
                };
                _ctx.Provincias.Add(prov1);
                _ctx.SaveChanges();

                var prov2 = new Provincia()
                {
                    Nombre = "Artemisa"
                };
                _ctx.Add(prov2);
                _ctx.SaveChanges();

                var prov3 = new Provincia()
                {
                    Nombre = "La Habana"
                };
                _ctx.Add(prov3);
                _ctx.SaveChanges();

                var prov4 = new Provincia()
                {
                    Nombre = "Mayabeque"
                };
                _ctx.Add(prov4);
                _ctx.SaveChanges();

                var prov5 = new Provincia()
                {
                    Nombre = "Guantanamo"
                };
                _ctx.Add(prov5);
                _ctx.SaveChanges();

                var prov6 = new Provincia()
                {
                    Nombre = "Santiago de Cuba"
                };
                _ctx.Add(prov6);
                _ctx.SaveChanges();

                var prov7 = new Provincia()
                {
                    Nombre = "Granma"
                };
                _ctx.Add(prov7);
                _ctx.SaveChanges();

                var prov8 = new Provincia()
                {
                    Nombre = "Las Tunas"
                };
                _ctx.Add(prov8);
                _ctx.SaveChanges();

                var prov9 = new Provincia()
                {
                    Nombre = "Las Villas"
                };
                _ctx.Add(prov9);
                _ctx.SaveChanges();

                var prov10 = new Provincia()
                {
                    Nombre = "Camaguey"
                };
                _ctx.Add(prov10);
                _ctx.SaveChanges();
    #endregion

                #region UNIDADES ORGANIZATIVAS
                var uo = new UnidadOrganizativa()
                {
                    Nombre = "Plaza",
                    Provincia = _ctx.Provincias.Find(3),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Playa",
                    Provincia = _ctx.Provincias.Find(3),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Pinar del Rio",
                    Provincia = _ctx.Provincias.Find(1),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Artemisa",
                    Provincia = _ctx.Provincias.Find(2),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Mayabeque",
                    Provincia = _ctx.Provincias.Find(4),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Guantanamo",
                    Provincia = _ctx.Provincias.Find(5),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Santiago de Cuba",
                    Provincia = _ctx.Provincias.Find(6),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Granma",
                    Provincia = _ctx.Provincias.Find(7),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Las Tunas",
                    Provincia = _ctx.Provincias.Find(8),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                uo = new UnidadOrganizativa()
                {
                    Nombre = "Las Villas",
                    Provincia = _ctx.Provincias.Find(9),
                    Inversionistas = new List<Usuario>()
                };
                _ctx.Add(uo);
                _ctx.SaveChanges();

                #endregion

                #region INMUEBLES
                var inm = new Inmueble()
                {
                    Direccion = "direccion1",
                    UO = _ctx.UnidadesOrganizativas.Find(1)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();

                inm = new Inmueble()
                {
                    Direccion = "direccion2",
                    UO = _ctx.UnidadesOrganizativas.Find(1)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();

                inm = new Inmueble()
                {
                    Direccion = "direccion3",
                    UO = _ctx.UnidadesOrganizativas.Find(2)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();

                inm = new Inmueble()
                {
                    Direccion = "direccion4",
                    UO = _ctx.UnidadesOrganizativas.Find(2)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();

                inm = new Inmueble()
                {
                    Direccion = "direccion5",
                    UO = _ctx.UnidadesOrganizativas.Find(3)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();

                inm = new Inmueble()
                {
                    Direccion = "direccion6",
                    UO = _ctx.UnidadesOrganizativas.Find(3)
                };
                _ctx.Add(inm);
                _ctx.SaveChanges();
    #endregion

                #region OBJETO_OBRA
                var obj = new ObjetoObra()
                {
                    Nombre = "objeto1",
                    Inmueble = _ctx.Inmuebles.Find(1)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto2",
                    Inmueble = _ctx.Inmuebles.Find(1)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto3",
                    Inmueble = _ctx.Inmuebles.Find(2)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto4",
                    Inmueble = _ctx.Inmuebles.Find(2)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto5",
                    Inmueble = _ctx.Inmuebles.Find(3)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto6",
                    Inmueble = _ctx.Inmuebles.Find(3)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto7",
                    Inmueble = _ctx.Inmuebles.Find(4)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto8",
                    Inmueble = _ctx.Inmuebles.Find(4)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto9",
                    Inmueble = _ctx.Inmuebles.Find(5)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto10",
                    Inmueble = _ctx.Inmuebles.Find(5)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto11",
                    Inmueble = _ctx.Inmuebles.Find(6)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto12",
                    Inmueble = _ctx.Inmuebles.Find(6)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto13",
                    Inmueble = _ctx.Inmuebles.Find(7)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();

                obj = new ObjetoObra()
                {
                    Nombre = "objeto14",
                    Inmueble = _ctx.Inmuebles.Find(7)
                };
                _ctx.Add(obj);
                _ctx.SaveChanges();
                #endregion

                #region PLANES
                var plan = new Plan()
                {
                    Año = 2019,
                    TipoPlan = "Reparación"
                };
                _ctx.Add(plan);
                _ctx.SaveChanges();

                plan = new Plan()
                {
                    Año = 2019,
                    TipoPlan = "Mantenimiento"
                };
                _ctx.Add(plan);
                _ctx.SaveChanges();
                #endregion

                #region ESPECIALIDADES
                var esp = new Especialidad()
                {
                    Tipo = "Albañilería"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Carpintería"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Herrería"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Electricidad"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Hidrosanitaria"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Falso techo"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Impermeabilización"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Pintura"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();

                esp = new Especialidad()
                {
                    Tipo = "Área exteriores"
                };
                _ctx.Add(esp);
                _ctx.SaveChanges();
                #endregion

                #region UNIDADES DE MEDIDA
                var um = new UnidadMedida()
                {
                    Nombre = "m2"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();

                um = new UnidadMedida()
                {
                    Nombre = "m3"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();

                um = new UnidadMedida()
                {
                    Nombre = "mL"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();

                um = new UnidadMedida()
                {
                    Nombre = "L"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();

                um = new UnidadMedida()
                {
                    Nombre = "g"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();

                um = new UnidadMedida()
                {
                    Nombre = "Kg"
                };
                _ctx.Add(um);
                _ctx.SaveChanges();
                #endregion

                #region MATERIALES
                var mat = new Material()
                {
                    Nombre = "cemento",
                    UnidadMedida = _ctx.UnidadesMedida.Find(6)
                };
                _ctx.Add(mat);
                _ctx.SaveChanges();

                mat = new Material()
                {
                    Nombre = "arena",
                    UnidadMedida = _ctx.UnidadesMedida.Find(6)
                };
                _ctx.Add(mat);
                _ctx.SaveChanges();

                mat = new Material()
                {
                    Nombre = "gravilla",
                    UnidadMedida = _ctx.UnidadesMedida.Find(6)
                };
                _ctx.Add(mat);
                _ctx.SaveChanges();

                mat = new Material()
                {
                    Nombre = "pintura roja",
                    UnidadMedida = _ctx.UnidadesMedida.Find(4)
                };
                _ctx.Add(mat);
                _ctx.SaveChanges();

                mat = new Material()
                {
                    Nombre = "pintura blanca",
                    UnidadMedida = _ctx.UnidadesMedida.Find(4)
                };
                _ctx.Add(mat);
                _ctx.SaveChanges();
                #endregion

                #region MANO DE OBRA
                var mob = new ManoObra()
                {
                    UnidadMedida = _ctx.UnidadesMedida.Find(2),
                    Cantidad = 100,
                    PrecioCUC = 5,
                    PrecioCUP = 125
                };
                _ctx.Add(mob);
                _ctx.SaveChanges();

                mob = new ManoObra()
                {
                    UnidadMedida = _ctx.UnidadesMedida.Find(1),
                    Cantidad = 100,
                    PrecioCUC = 5,
                    PrecioCUP = 125
                };
                _ctx.Add(mob);
                _ctx.SaveChanges();
                #endregion

                #region ACCIONES CONSTRUCTIVAS
                var acc = new AccionConstructiva()
                {
                    Nombre = "accion1",
                    Plan = _ctx.Planes.Find(1),
                    ObjetoObra = _ctx.ObjetosObra.Find(1),
                    Especialidad = _ctx.Especialidades.Find(1),
                    ManoObra = _ctx.ManosObra.Find(1)
                };
                _ctx.Add(acc);
                _ctx.SaveChanges();

                acc = new AccionConstructiva()
                {
                    Nombre = "accion2",
                    Plan = _ctx.Planes.Find(2),
                    ObjetoObra = _ctx.ObjetosObra.Find(1),
                    Especialidad = _ctx.Especialidades.Find(2),
                    ManoObra = _ctx.ManosObra.Find(1)
                };
                _ctx.Add(acc);
                _ctx.SaveChanges();

                acc = new AccionConstructiva()
                {
                    Nombre = "accion3",
                    Plan = _ctx.Planes.Find(2),
                    ObjetoObra = _ctx.ObjetosObra.Find(1),
                    Especialidad = _ctx.Especialidades.Find(1),
                    ManoObra = _ctx.ManosObra.Find(2)
                };
                _ctx.Add(acc);
                _ctx.SaveChanges();
                #endregion

                #region ACC_CONS-MATERIALES
                var accons = new AccionC_Material()
                {
                    Material = _ctx.Materiales.Find(1),
                    AccionConstructiva = _ctx.AccionesCons.Find(1),
                    Cantidad = 100,
                    PrecioCUC = 4,
                    PrecioCUP = 100
                };
                _ctx.Add(accons);
                _ctx.SaveChanges();

                accons = new AccionC_Material()
                {
                    Material = _ctx.Materiales.Find(2),
                    AccionConstructiva = _ctx.AccionesCons.Find(1),
                    Cantidad = 100,
                    PrecioCUC = 4,
                    PrecioCUP = 100
                };
                _ctx.Add(accons);
                _ctx.SaveChanges();

                accons = new AccionC_Material()
                {
                    Material = _ctx.Materiales.Find(3),
                    AccionConstructiva = _ctx.AccionesCons.Find(1),
                    Cantidad = 100,
                    PrecioCUC = 4,
                    PrecioCUP = 100
                };
                _ctx.Add(accons);
                _ctx.SaveChanges();

                accons = new AccionC_Material()
                {
                    Material = _ctx.Materiales.Find(4),
                    AccionConstructiva = _ctx.AccionesCons.Find(2),
                    Cantidad = 100,
                    PrecioCUC = 4,
                    PrecioCUP = 100
                };
                _ctx.Add(accons);
                _ctx.SaveChanges();

                accons = new AccionC_Material()
                {
                    Material = _ctx.Materiales.Find(5),
                    AccionConstructiva = _ctx.AccionesCons.Find(3),
                    Cantidad = 100,
                    PrecioCUC = 4,
                    PrecioCUP = 100
                };
                _ctx.Add(accons);
                _ctx.SaveChanges();
                #endregion

                //    /*var filepath = Path.Combine(_hosting.ContentRootPath, "wwwroot/json/usuarios.json");
                //    var json = File.ReadAllText(filepath);
                //    var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);

                //    foreach (var user in usuarios)
                //    {
                //        _userManager.CreateAsync(user, "1234");
                //       // _userManager.AddClaimAsync(user, new Claim("Permission", "common"));
                //    }*/

                _ctx.SaveChanges();
            }

        }
    }
}
