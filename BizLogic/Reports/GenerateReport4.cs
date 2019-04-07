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
                                               objetos = from obj in inm.ObjetosDeObra
                                                         select new
                                                         {
                                                             nombre = obj.Nombre,
                                                             materiales = from ac in obj.AccionesConstructivas
                                                                          from acm in ac.Materiales
                                                                          select new
                                                                          {
                                                                              nombre = acm.Material.Nombre,
                                                                              unidadMedida = acm.Material.UnidadMedida,
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
}
