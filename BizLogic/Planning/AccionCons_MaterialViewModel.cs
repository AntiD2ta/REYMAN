using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionCons_MaterialViewModel
    {
        public int AccionConstructivaID { get; set; }
        public int AccionCons_MaterialID { get; set; } 
        public decimal? PrecioCUP { get; set; }
        public decimal? PrecioCUC { get; set; }
        public decimal? Cantidad { get; set; }

        public string Button { get; set; }
    }
}
