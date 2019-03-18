using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class AccionConstructiva
    {
        public int AccionConstructivaID { get; set; }
        public string Especialidad { get; set; }
        public string Nombre { get; set; }

        public virtual ObjetoObra ObjetoObra { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ICollection<AccionC_Material> Materiales { get; set; }
        public virtual ICollection<ManoObra> ManoObra { get; set; }
    }
}
