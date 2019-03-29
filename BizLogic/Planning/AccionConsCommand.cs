using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionConsCommand : AccionConsViewModel
    {
        public Plan Plan { get; set; }
        public Especialidad Especialidad { get; set; }
        public ObjetoObra ObjetoObra { get; set; }
        public ManoObra ManoObra { get; set; }
        public List<(Material material, Decimal? precio)> MaterialPrecio { get; set; }

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
                Precio = Precio,
                UnidadMedida = new UnidadMedida()
                {
                    Nombre = UM
                }
            };
        }

        public IEnumerable<(Material material, Decimal? precio)> ToAC_M()
        {
            foreach(var t in Materiales)
            {
                var material = new Material()
                {
                    Nombre = t.nameMaterial,
                    UnidadMedida = new UnidadMedida()
                    {
                        Nombre = t.unidadMedida
                    }
                };

                yield return (material, t.precio);
            }
        }
    }
}
