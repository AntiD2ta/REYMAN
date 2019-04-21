using BizData.Entities;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Reports
{
    public class GenerateReport4
    {
        private readonly EfCoreContext _context;

        public GenerateReport4(EfCoreContext context)
        {
            _context = context;
        }

        public ReportFour GenerateReport(int year, IEnumerable<string> UOs)
        {
            var uos = from name in UOs
                      from unidad in _context.UnidadesOrganizativas
                      where name == unidad.Nombre
                      select unidad;

            var report = new ReportFour
            {
                unidades = from unidad in uos
                           select new ReportFourUnidad
                           {
                               Nombre = unidad.Nombre,
                               inmuebles = from inm in unidad.Inmuebles
                                           select new ReportFourInmueble
                                           {
                                               Nombre = inm.Direccion,
                                               objetos = from obj in inm.ObjetosDeObra
                                                         select new ReportFourObra
                                                         {
                                                             Nombre = obj.Nombre,
                                                             materiales = from ac in obj.AccionesConstructivas
                                                                          from acm in ac.Materiales
                                                                          select new ReportFourMaterial
                                                                          {
                                                                              Nombre = acm.Material.Nombre,
                                                                              unidadMedida = acm.Material.UnidadMedida.Nombre,
                                                                              reparaciones = (from mat in ac.Materiales
                                                                                              where mat.Material.MaterialID == acm.Material.MaterialID && ac.Plan.TipoPlan == "Reparación"
                                                                                              select mat.Cantidad).Sum(),
                                                                              mantenimiento = (from mat in ac.Materiales
                                                                                               where mat.Material.MaterialID == acm.Material.MaterialID && ac.Plan.TipoPlan == "Mantenimiento"
                                                                                               select mat.Cantidad).Sum()
                                                                          }
                                                         }
                                           }
                           },
                año = year
            };

            return report;
        }
    }

    public class ReportFourMaterial
    {
        public string Nombre { get; set; }
        public string unidadMedida { get; set; }
        public decimal? reparaciones { get; set; }
        public decimal? mantenimiento { get; set; }
    }

    public class ReportFourObra
    {
        public string Nombre { get; set; }
        public IEnumerable<ReportFourMaterial> materiales { get; set; }
    }

    public class ReportFourInmueble
    {
        public string Nombre { get; set; }
        public IEnumerable<ReportFourObra> objetos { get; set; }
    }
    
    public class ReportFourUnidad
    {
        public string Nombre { get; set; }
        public IEnumerable<ReportFourInmueble> inmuebles { get; set; }
    }

    public class ReportFour
    {
        public IEnumerable<ReportFourUnidad> unidades { get; set; }
        public int año { get; set; }
    }
}
