using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string Nombre { get; set; }

        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual ICollection<AccionC_Material> AccionesConstructivas { get; set; }
    }
}
