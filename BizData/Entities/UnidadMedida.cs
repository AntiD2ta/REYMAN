using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class UnidadMedida
    {
        public int UnidadMedidaID { get; set; }
        public string Nombre { get; set; }
    }

    public class UMComparer : IEqualityComparer<UnidadMedida>
    {
        public bool Equals(UnidadMedida x, UnidadMedida y) => x.Nombre == y.Nombre;

        public int GetHashCode(UnidadMedida obj) => obj.Nombre.GetHashCode();
    }
}
