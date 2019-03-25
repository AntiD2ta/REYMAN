using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class UnidadOrganizativa
    {
        public int UnidadOrganizativaID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Especialidad> Especialidades { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual ICollection<Usuario> Inversionistas { get; set; }
        public virtual ICollection<Inmueble> Inmuebles { get; set; }
    }
}
