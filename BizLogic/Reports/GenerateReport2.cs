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

        public object GenerateReport(int year, IEnumerable<UnidadOrganizativa> uos)
        {
            var report = new
            {
                unidades = from unidad in uos
                           select new
                           {
                               nombre = unidad.Nombre,
                               inmuebles = from inm in unidad.Inmuebles
                                           select new
                                           {
                                               nombre = inm.Direccion,
                                               reparacionesCUC = (from obj in inm.ObjetosDeObra
                                                                 from ac in obj.AccionesConstructivas
                                                                 where ac.Plan.TipoPlan == "Reparación"
                                                                 select ac.ManoObra.PrecioCUC).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                        from ac in obj.AccionesConstructivas
                                                                                                        where ac.Plan.TipoPlan == "Reparación"
                                                                                                        from acm in ac.Materiales
                                                                                                        select acm.PrecioCUC).Sum(),
                                               reparacionesCUP = (from obj in inm.ObjetosDeObra
                                                                  from ac in obj.AccionesConstructivas
                                                                  where ac.Plan.TipoPlan == "Reparación"
                                                                  select ac.ManoObra.PrecioCUP).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                         from ac in obj.AccionesConstructivas
                                                                                                         where ac.Plan.TipoPlan == "Reparación"
                                                                                                         from acm in ac.Materiales
                                                                                                         select acm.PrecioCUP).Sum(),
                                               mantenimientoCUC = (from obj in inm.ObjetosDeObra
                                                                  from ac in obj.AccionesConstructivas
                                                                  where ac.Plan.TipoPlan == "Mantenimiento"
                                                                   select ac.ManoObra.PrecioCUC).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                         from ac in obj.AccionesConstructivas
                                                                                                         where ac.Plan.TipoPlan == "Mantenimiento"
                                                                                                          from acm in ac.Materiales
                                                                                                         select acm.PrecioCUC).Sum(),
                                               mantenimientoCUP = (from obj in inm.ObjetosDeObra
                                                                   from ac in obj.AccionesConstructivas
                                                                   where ac.Plan.TipoPlan == "Mantenimiento"
                                                                   select ac.ManoObra.PrecioCUP).Sum() + (from obj in inm.ObjetosDeObra
                                                                                                          from ac in obj.AccionesConstructivas
                                                                                                          where ac.Plan.TipoPlan == "Mantenimiento"
                                                                                                          from acm in ac.Materiales
                                                                                                          select acm.PrecioCUP).Sum()
                                           },
                               reparacionesCUC = (from inm in unidad.Inmuebles
                                                  from obj in inm.ObjetosDeObra
                                                  from ac in obj.AccionesConstructivas
                                                  where ac.Plan.TipoPlan == "Reparación"
                                                  select ac.ManoObra.PrecioCUC).Sum() + (from inm in unidad.Inmuebles
                                                                                         from obj in inm.ObjetosDeObra
                                                                                         from ac in obj.AccionesConstructivas
                                                                                         where ac.Plan.TipoPlan == "Reparación"
                                                                                         from acm in ac.Materiales
                                                                                         select acm.PrecioCUC).Sum(),
                               reparacionesCUP = (from inm in unidad.Inmuebles
                                                  from obj in inm.ObjetosDeObra
                                                  from ac in obj.AccionesConstructivas
                                                  where ac.Plan.TipoPlan == "Reparación"
                                                  select ac.ManoObra.PrecioCUP).Sum() + (from inm in unidad.Inmuebles
                                                                                         from obj in inm.ObjetosDeObra
                                                                                         from ac in obj.AccionesConstructivas
                                                                                         where ac.Plan.TipoPlan == "Reparación"
                                                                                         from acm in ac.Materiales
                                                                                         select acm.PrecioCUP).Sum(),
                               mantenimientoCUC = (from inm in unidad.Inmuebles
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == "Mantenimiento"
                                                   select ac.ManoObra.PrecioCUC).Sum() + (from inm in unidad.Inmuebles
                                                                                          from obj in inm.ObjetosDeObra
                                                                                          from ac in obj.AccionesConstructivas
                                                                                          where ac.Plan.TipoPlan == "Mantenimiento"
                                                                                          from acm in ac.Materiales
                                                                                          select acm.PrecioCUC).Sum(),
                               mantenimientoCUP = (from inm in unidad.Inmuebles
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == "Mantenimiento"
                                                   select ac.ManoObra.PrecioCUP).Sum() + (from inm in unidad.Inmuebles
                                                                                          from obj in inm.ObjetosDeObra
                                                                                          from ac in obj.AccionesConstructivas
                                                                                          where ac.Plan.TipoPlan == "Mantenimiento"
                                                                                          from acm in ac.Materiales
                                                                                          select acm.PrecioCUP).Sum()
                           },
                año = year
            };

            return report;
        }
    }
}
