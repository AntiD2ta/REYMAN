using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class AccionC_Material
    {
        public int AccionC_MaterialID { get; set; }
        public decimal? PrecioCUP { get; set; }
        public decimal? PrecioCUC { get; set; }
        public decimal? Cantidad { get; set; }

        public virtual AccionConstructiva AccionConstructiva { get; set; }
        public virtual Material Material { get; set; }
    }
}