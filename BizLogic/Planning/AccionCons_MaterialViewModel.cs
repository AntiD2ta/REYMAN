﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionCons_MaterialViewModel
    {
        public int ID { get; set; }
        public decimal? PrecioCUP { get; set; }
        public decimal? PrecioCUC { get; set; }
        public decimal? Cantidad { get; set; }

        public string Button { get; set; }
    }
}
