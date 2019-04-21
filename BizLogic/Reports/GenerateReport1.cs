using System.Collections.Generic;
using BizData.Entities;
using DataLayer.EfCode;
using System.Linq;

namespace BizLogic.Reports
{
    public class GenerateReport1
    {
        private readonly EfCoreContext _context;

        public GenerateReport1(EfCoreContext context)
        {
            _context = context;
        }

        public ReportOne GenerateReport(int year, string tipoPlan, IEnumerable<string> UOs, IEnumerable<string> Inmuebles)
        {
            var uos = from name in UOs
                      from unidad in _context.UnidadesOrganizativas
                      where name == unidad.Nombre
                      select unidad;

            var inmuebles = from inm in Inmuebles
                            from unidad in uos
                            from inmb in unidad.Inmuebles
                            where inm == inmb.Direccion
                            select inmb;

            var report = new ReportOne()
            {
                unidades = from unidad in uos
                           select new ReportOneUnidad()
                           {
                               Nombre = unidad.Nombre,
                               inmuebles = from inm in inmuebles
                                           where inm.UO.UnidadOrganizativaID == unidad.UnidadOrganizativaID
                                           select new ReportOneInmueble()
                                           {
                                               Nombre = inm.Direccion,
                                               objetos = from obj in inm.ObjetosDeObra
                                                         select new ReportOneObject()
                                                         {
                                                             Nombre = obj.Nombre,
                                                             especialidades = from espld in (from esp in _context.Especialidades
                                                                                              select new ReportOneEspecialidad()
                                                                                              {
                                                                                                  Nombre = esp.Tipo,
                                                                                                  acciones = from ac in obj.AccionesConstructivas
                                                                                                             where ac.Especialidad.EspecialidadID == esp.EspecialidadID && ac.Plan.TipoPlan == tipoPlan
                                                                                                             select new ReportOneAccion()
                                                                                                             {
                                                                                                                 Nombre = ac.Nombre,
                                                                                                                 manoObraCUC = ac.ManoObra.PrecioCUC,
                                                                                                                 manoObraCUP = ac.ManoObra.PrecioCUP,
                                                                                                                 materialesCUC = (from acm in ac.Materiales
                                                                                                                                  select acm.PrecioCUC).Sum(),
                                                                                                                 materialesCUP = (from acm in ac.Materiales
                                                                                                                                  where acm.AccionConstructiva.AccionConstructivaID == ac.AccionConstructivaID
                                                                                                                                  select acm.PrecioCUP).Sum(),
                                                                                                                 unidadMedida = ac.ManoObra.UnidadMedida.Nombre,
                                                                                                                 cantidad = ac.ManoObra.Cantidad
                                                                                                             },
                                                                                                  manoObraTotalCUC = (from ac in obj.AccionesConstructivas
                                                                                                                      where ac.Especialidad.EspecialidadID == esp.EspecialidadID && ac.Plan.TipoPlan == tipoPlan
                                                                                                                      select ac.ManoObra.PrecioCUC).Sum(),
                                                                                                  manoObraTotalCUP = (from ac in obj.AccionesConstructivas
                                                                                                                      where ac.Especialidad.EspecialidadID == esp.EspecialidadID && ac.Plan.TipoPlan == tipoPlan
                                                                                                                      select ac.ManoObra.PrecioCUP).Sum(),
                                                                                                  materialesTotalCUC = (from ac in obj.AccionesConstructivas
                                                                                                                        where ac.Plan.TipoPlan == tipoPlan
                                                                                                                        from mat in ac.Materiales
                                                                                                                        select mat.PrecioCUC).Sum(),
                                                                                                  materialesTotalCUP = (from ac in obj.AccionesConstructivas
                                                                                                                        where ac.Plan.TipoPlan == tipoPlan
                                                                                                                        from mat in ac.Materiales
                                                                                                                        select mat.PrecioCUP).Sum()
                                                                                              })
                                                                              where espld.manoObraTotalCUC != 0 && espld.materialesTotalCUC != 0
                                                                              select espld,
                                                             manoObraTotalCUC = (from ac in obj.AccionesConstructivas
                                                                                 where ac.Plan.TipoPlan == tipoPlan
                                                                                 select ac.ManoObra.PrecioCUC).Sum(),
                                                             materialesTotalCUC = (from ac in obj.AccionesConstructivas
                                                                                   where ac.Plan.TipoPlan == tipoPlan
                                                                                   from acm in ac.Materiales
                                                                                   select acm.PrecioCUC).Sum(),
                                                             manoObraTotalCUP = (from ac in obj.AccionesConstructivas
                                                                                 where ac.Plan.TipoPlan == tipoPlan
                                                                                 select ac.ManoObra.PrecioCUP).Sum(),
                                                             materialesTotalCUP = (from ac in obj.AccionesConstructivas
                                                                                   where ac.Plan.TipoPlan == tipoPlan
                                                                                   from acm in ac.Materiales
                                                                                   select acm.PrecioCUP).Sum()
                                                         },
                                               manoObraTotalCUC = (from obj in inm.ObjetosDeObra
                                                                   from ac in obj.AccionesConstructivas
                                                                   where ac.Plan.TipoPlan == tipoPlan
                                                                   select ac.ManoObra.PrecioCUC).Sum(),
                                               materialesTotalCUC = (from obj in inm.ObjetosDeObra
                                                                     from ac in obj.AccionesConstructivas
                                                                     where ac.Plan.TipoPlan == tipoPlan
                                                                     from acm in ac.Materiales
                                                                     select acm.PrecioCUC).Sum(),
                                               manoObraTotalCUP = (from obj in inm.ObjetosDeObra
                                                                   from ac in obj.AccionesConstructivas
                                                                   where ac.Plan.TipoPlan == tipoPlan
                                                                   select ac.ManoObra.PrecioCUP).Sum(),
                                               materialesTotalCUP = (from obj in inm.ObjetosDeObra
                                                                     from ac in obj.AccionesConstructivas
                                                                     where ac.Plan.TipoPlan == tipoPlan
                                                                     from acm in ac.Materiales
                                                                     select acm.PrecioCUP).Sum()
                                           },
                               manoObraTotalCUC = (from inm in inmuebles
                                                   where inm.UO.UnidadOrganizativaID == unidad.UnidadOrganizativaID
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == tipoPlan
                                                   select ac.ManoObra.PrecioCUC).Sum(),
                               materialesTotalCUC = (from inm in inmuebles
                                                     where inm.UO.UnidadOrganizativaID == unidad.UnidadOrganizativaID
                                                     from obj in inm.ObjetosDeObra
                                                     from ac in obj.AccionesConstructivas
                                                     where ac.Plan.TipoPlan == tipoPlan
                                                     from acm in ac.Materiales
                                                     select acm.PrecioCUC).Sum(),
                               manoObraTotalCUP = (from inm in inmuebles
                                                   where inm.UO.UnidadOrganizativaID == unidad.UnidadOrganizativaID
                                                   from obj in inm.ObjetosDeObra
                                                   from ac in obj.AccionesConstructivas
                                                   where ac.Plan.TipoPlan == tipoPlan
                                                   select ac.ManoObra.PrecioCUP).Sum(),
                               materialesTotalCUP = (from inm in inmuebles
                                                     where inm.UO.UnidadOrganizativaID == unidad.UnidadOrganizativaID
                                                     from obj in inm.ObjetosDeObra
                                                     from ac in obj.AccionesConstructivas
                                                     where ac.Plan.TipoPlan == tipoPlan
                                                     from acm in ac.Materiales
                                                     select acm.PrecioCUP).Sum()
                           },
                tipo = tipoPlan,
                año = year
            };
            return report;
        }
    }

    public class ReportOneAccion
    {
        public ReportOneAccion()
        {
        }

        public string Nombre { get; set; }
        public decimal? manoObraCUC { get; set; }
        public decimal? manoObraCUP { get; set; }
        public decimal? materialesCUC { get; set; }
        public decimal? materialesCUP { get; set; }
        public string unidadMedida { get; set; }
        public int cantidad { get; set; }
    }

    public class ReportOneEspecialidad
    {
        public ReportOneEspecialidad()
        {
        }

        public string Nombre { get; set; }
        public IEnumerable<ReportOneAccion> acciones { get; set; }
        public decimal? manoObraTotalCUC { get; set; }
        public decimal? manoObraTotalCUP { get; set; }
        public decimal? materialesTotalCUC { get; set; }
        public decimal? materialesTotalCUP { get; set; }
    }

    public class ReportOneObject
    {
        public ReportOneObject()
        {
        }

        public string Nombre { get; set; }
        public IQueryable<ReportOneEspecialidad> especialidades { get; set; }
        public decimal? manoObraTotalCUC { get; set; }
        public decimal? materialesTotalCUC { get; set; }
        public decimal? manoObraTotalCUP { get; set; }
        public decimal? materialesTotalCUP { get; set; }
    }

    public class ReportOneInmueble
    {
        public ReportOneInmueble()
        {
        }

        public string Nombre { get; set; }
        public IEnumerable<ReportOneObject> objetos { get; set; }
        public decimal? manoObraTotalCUC { get; set; }
        public decimal? materialesTotalCUC { get; set; }
        public decimal? manoObraTotalCUP { get; set; }
        public decimal? materialesTotalCUP { get; set; }
    }

    public class ReportOneUnidad
    {
        public ReportOneUnidad()
        {
        }

        public string Nombre { get; set; }
        public IEnumerable<ReportOneInmueble> inmuebles { get; set; }
        public decimal? manoObraTotalCUC { get; set; }
        public decimal? materialesTotalCUC { get; set; }
        public decimal? manoObraTotalCUP { get; set; }
        public decimal? materialesTotalCUP { get; set; }
    }

    public class ReportOne 
    {
        public ReportOne()
        {
        }

        public IEnumerable<ReportOneUnidad> unidades { get; set; }
        public string tipo { get; set; }
        public int año { get; set; }
    }
}
