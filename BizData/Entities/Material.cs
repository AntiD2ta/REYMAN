using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }

        public virtual ICollection<AccionConstructiva> AccionesConstructivas { get; set; }
    }
}
