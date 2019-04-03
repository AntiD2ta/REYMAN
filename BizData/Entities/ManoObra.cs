using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class ManoObra
    {
        public int ManoObraID { get; set; }
        public int Cantidad { get; set; }
        public Decimal? PrecioCUP { get; set; }
        public Decimal? PrecioCUC { get; set; }

        public virtual UnidadMedida UnidadMedida { get; set; }
    }
}
