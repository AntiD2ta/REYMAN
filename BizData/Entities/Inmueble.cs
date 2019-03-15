using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Inmueble
    {
        public int InmuebleID { get; set; }
        public string Direccion { get; set; }

        public virtual UnidadOrganizativa UO { get; set; }
        public virtual ICollection<ObjetoObra> ObjetosDeObra { get; set; }
    }
}
