using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class UnidadOrganizativa
    {
        public int UnidadOrganizativaID { get; set; }
        public string Nombre { get; set; }
        
        public virtual Provincia Provincia { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Inmueble> Inmuebles { get; set; }
        public virtual ICollection<Plan> Planes { get; set; }
    }
}
