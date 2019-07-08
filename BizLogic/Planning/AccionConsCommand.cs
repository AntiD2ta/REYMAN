using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionConsCommand : AccionConsViewModel
    {
        public int AccionConstructivaID { get; set; }
        public Plan Plan { get; set; }
        public Especialidad Especialidad { get; set; }
        public ObjetoObra ObjetoObra { get; set; }
        public ManoObra ManoObra { get; set; }
        public List<(Material material, Decimal? precioCUP, Decimal? precioCUC)> MaterialPrecio { get; set; }

        public AccionConstructiva ToAC()
        {
            return new AccionConstructiva()
            {
                Nombre = Nombre,
                Especialidad = Especialidad,
                Plan = Plan,
                ObjetoObra = ObjetoObra,
                ManoObra = ManoObra,
            };
        }

        public ManoObra ToManoObra()
        {
            return new ManoObra()
            {
                Cantidad = CantidadMO,
                PrecioCUP = PrecioCUP,
                PrecioCUC = PrecioCUC,
                UnidadMedida = new UnidadMedida()
                {
                    Nombre = UM
                }
            };
        }

        public IEnumerable<(Material material, Decimal? precioCUP, Decimal? precioCUC)> ToAC_M(List<UnidadMedida> ums, List<Material> mat)
        {
            foreach(var t in Materiales)
            {
                Material material = mat.Where(m => m.Nombre == t.nameMaterial && m.UnidadMedida.Nombre == t.unidadMedida).SingleOrDefault();

                if (material == null)
                {
                    material = new Material()
                    {
                        Nombre = t.nameMaterial,
                        UnidadMedida = ums.Find(um => um.Nombre == t.unidadMedida)
                    };                  
                }

                yield return (material, t.precioCUP, t.precioCUC);
            }
        }

    }
}
