using BizData.Entities;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BizLogic.Reports
{
    public class GenerateReport5
    {
        private readonly EfCoreContext _context;

        public GenerateReport5(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<ReportFive> GenerateReport(int year, IEnumerable<string> UOs)
        {
            var uos = from name in UOs
                      from unidad in _context.UnidadesOrganizativas
                      where name == unidad.Nombre
                      select unidad;
            
            var report = new ReportFive
            {
                materiales = from mat in _context.Materiales
                             select new ReportFiveMaterial
                             {
                                 Nombre = mat.Nombre,
                                 unidadMedida = mat.UnidadMedida.Nombre
                             },

                unidades = from unidad in uos
                           select new ReportFiveUnidad
                           {
                               Nombre = unidad.Nombre,
                               materiales = from mat in _context.Materiales
                                            select (from inm in unidad.Inmuebles
                                                    from obj in inm.ObjetosDeObra
                                                    from ac in obj.AccionesConstructivas
                                                    from acm in ac.Materiales
                                                    where mat.MaterialID == acm.Material.MaterialID && ac.Plan.Año == year
                                                    select acm.Cantidad).Sum()
                           },

                totales = from mat in (await _context.Materiales.ToListAsync())
                          select (from unidad in uos
                                  from inm in unidad.Inmuebles
                                  from obj in inm.ObjetosDeObra
                                  from ac in obj.AccionesConstructivas
                                  from acm in ac.Materiales
                                  where mat.MaterialID == acm.Material.MaterialID && ac.Plan.Año == year
                                  select acm.Cantidad).Sum(),
                año = year
            };

            return report;
        }
    }

    public class ReportFiveUnidad
    {
        public string Nombre { get; set; }
        public IQueryable<decimal?> materiales { get; set; }
    }

    public class ReportFiveMaterial
    {
        public string Nombre { get; set; }
        public string unidadMedida { get; set; }
    }

    public class ReportFive
    {
        public IQueryable<ReportFiveMaterial> materiales { get; set; }
        public IEnumerable<ReportFiveUnidad> unidades { get; set; }
        public IEnumerable<decimal?> totales { get; set; }
        public int año { get; set; }
    }
}
