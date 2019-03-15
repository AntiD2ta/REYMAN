using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Provincia
    {
        public int ProvinciaID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UnidadOrganizativa> UnidadesOrganizativas { get; set; }
    }
}
