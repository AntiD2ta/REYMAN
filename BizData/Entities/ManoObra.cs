using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class ManoObra
    {
        public int ManoObraID { get; set; }
        public int Cantidad { get; set; }
        public decimal? PrecioCUP { get; set; }
        public decimal? PrecioCUC { get; set; }

        public virtual UnidadMedida UnidadMedida { get; set; }
    }
}
