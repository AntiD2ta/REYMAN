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

        public async Task<object> GenerateReport(int year, IEnumerable<string> UOs)
        {
            var uos = from name in UOs
                      from unidad in _context.UnidadesOrganizativas
                      where name == unidad.Nombre
                      select unidad;
            
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

                totales = from mat in (await _context.Materiales.ToListAsync())
                          select (from unidad in uos
                                  from inm in unidad.Inmuebles
                                  from obj in inm.ObjetosDeObra
                                  from ac in obj.AccionesConstructivas
                                  from acm in ac.Materiales
                                  where mat.MaterialID == acm.Material.MaterialID
                                  select acm.Cantidad).Sum(),
                año = year
            };

            return report;
        }

        //private async Task<object> Test(EfCoreContext context, IEnumerable<UnidadOrganizativa> uos)
        //{
        //    return  
        //}
    }
}
