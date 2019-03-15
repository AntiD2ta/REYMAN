using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class ObjetoObra
    {
        public int ObjetoObraID { get; set; }

        public virtual Inmueble Inmueble { get; set; }
        public virtual ICollection<AccionConstructiva> AccionesConstructivas { get; set; }
    }
}
