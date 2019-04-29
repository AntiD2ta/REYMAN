using BizData.Entities;
using DataLayer.EfCode;
using System.Collections.Generic;
using System.Linq;

namespace BizLogic.Reports
{
    public class GenerateReport2
    {
        private readonly EfCoreContext _context;

        public GenerateReport2(EfCoreContext context)
        {
            _context = context;
        }

        public ReportTwo GenerateReport(int year, IEnumerable<string> UOs)
        {
            var uos = from name in UOs
                      from unidad in _context.UnidadesOrganizativas
                      where name == unidad.Nombre
                      select unidad;

            var report = new ReportTwo
            {
                unidades = from unidad in uos
                           select new ReportTwoUnidad
                           {
                               Nombre = unidad.Nombre,
                               inmuebles = from inm in unidad.Inmuebles
                                           select new ReportTwoInmueble
                                           {
                                               Nombre = inm.Direccion,
                                               reparacionesCUC = (from obj in inm.ObjetosDeObra
                                                                  from ac in obj.AccionesConstructivas
                                                                  where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                  select ac.ManoObra.PrecioCUC).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                         from ac in obj.AccionesConstructivas
                                                                                                         where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                                                         from acm in ac.Materiales
                                                                                                         select acm.PrecioCUC).Sum(),
                                               reparacionesCUP = (from obj in inm.ObjetosDeObra
                                                                  from ac in obj.AccionesConstructivas
                                                                  where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                  select ac.ManoObra.PrecioCUP).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                         from ac in obj.AccionesConstructivas
                                                                                                         where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                                                         from acm in ac.Materiales
                                                                                                         select acm.PrecioCUP).Sum(),
                                               mantenimientoCUC = (from obj in inm.ObjetosDeObra
                                                                   from ac in obj.AccionesConstructivas
                                                                   where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                   select ac.ManoObra.PrecioCUC).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                          from ac in obj.AccionesConstructivas
                                                                                                          where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                                                          from acm in ac.Materiales
                                                                                                          select acm.PrecioCUC).Sum(),
                                               mantenimientoCUP = (from obj in inm.ObjetosDeObra
                                                                   from ac in obj.AccionesConstructivas
                                                                   where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                   select ac.ManoObra.PrecioCUP).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                          from ac in obj.AccionesConstructivas
                                                                                                          where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                                                          from acm in ac.Materiales
                                                                                                          select acm.PrecioCUP).Sum()
                                           },
                               reparacionesCUC = (from inm in unidad.Inmuebles
                                                  from obj in inm.ObjetosDeObra
                                                  from ac in obj.AccionesConstructivas
                                                  where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                  select ac.ManoObra.PrecioCUC).Sum() + (from inm in unidad.Inmuebles
                                                                                         from obj in inm.ObjetosDeObra
                                                                                         from ac in obj.AccionesConstructivas
                                                                                         where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                                         from acm in ac.Materiales
                                                                                         select acm.PrecioCUC).Sum(),
                               reparacionesCUP = (from inm in unidad.Inmuebles
                                                  from obj in inm.ObjetosDeObra
                                                  from ac in obj.AccionesConstructivas
                                                  where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                  select ac.ManoObra.PrecioCUP).Sum() + (from inm in unidad.Inmuebles
                                                                                         from obj in inm.ObjetosDeObra
                                                                                         from ac in obj.AccionesConstructivas
                                                                                         where ac.Plan.TipoPlan == "Reparación" && ac.Plan.Año == year
                                                                                         from acm in ac.Materiales
                                                                                         select acm.PrecioCUP).Sum(),
                               mantenimientoCUC = (from inm in unidad.Inmuebles
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                   select ac.ManoObra.PrecioCUC).Sum() + (from inm in unidad.Inmuebles
                                                                                          from obj in inm.ObjetosDeObra
                                                                                          from ac in obj.AccionesConstructivas
                                                                                          where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                                          from acm in ac.Materiales
                                                                                          select acm.PrecioCUC).Sum(),
                               mantenimientoCUP = (from inm in unidad.Inmuebles
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                   select ac.ManoObra.PrecioCUP).Sum() + (from inm in unidad.Inmuebles
                                                                                          from obj in inm.ObjetosDeObra
                                                                                          from ac in obj.AccionesConstructivas
                                                                                          where ac.Plan.TipoPlan == "Mantenimiento" && ac.Plan.Año == year
                                                                                          from acm in ac.Materiales
                                                                                          select acm.PrecioCUP).Sum()
                           },
                año = year
            };

            return report;
        }
    }

    public class ReportTwoInmueble
    {
        public string Nombre { get; set; }
        public decimal? reparacionesCUC { get; set; }
        public decimal? reparacionesCUP { get; set; }
        public decimal? mantenimientoCUC { get; set; }
        public decimal? mantenimientoCUP { get; set; }
    }

    public class ReportTwoUnidad
    {
        public string Nombre { get; set; }
        public IEnumerable<ReportTwoInmueble> inmuebles { get; set; }
        public decimal? reparacionesCUC { get; set; }
        public decimal? reparacionesCUP { get; set; }
        public decimal? mantenimientoCUC { get; set; }
        public decimal? mantenimientoCUP { get; set; }
    }

    public class ReportTwo
    {
        public IEnumerable<ReportTwoUnidad> unidades { get; set; }
        public int año { get; set; }
    }
}
