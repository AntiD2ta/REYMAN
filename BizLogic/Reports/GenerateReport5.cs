using BizData.Entities;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Reports
{
    public class GenerateReport5
    {
        private readonly EfCoreContext _context;

        public GenerateReport5(EfCoreContext context)
        {
            _context = context;
        }

        public object GenerateReport(int year, IEnumerable<UnidadOrganizativa> uos)
        {
            var report = new
            {
                materiales = from mat in _context.Materiales
                             select new
                             {
                                 nombre = mat.Nombre,
                                 unidadMedida = mat.UnidadMedida.Nombre
                             },

                unidades = from unidad in uos
                           select new
                           {
                               nombre = unidad.Nombre,
                               materiales = from mat in _context.Materiales
                                            select (from inm in unidad.Inmuebles
                                                    from obj in inm.ObjetosDeObra
                                                    from ac in obj.AccionesConstructivas
                                                    from acm in ac.Materiales
                                                    where mat.MaterialID == acm.Material.MaterialID
                                                    select acm.Cantidad).Sum()
                           },
                totales = (from unidad in uos
                           select (from mat in _context.Materiales
                                   select (from inm in unidad.Inmuebles
                                           from obj in inm.ObjetosDeObra
                                           from ac in obj.AccionesConstructivas
                                           from acm in ac.Materiales
                                           where mat.MaterialID == acm.Material.MaterialID
                                           select acm.Cantidad).Sum()).Sum()).Sum(),
                año = year
            };

            return report;
        }
    }
}
