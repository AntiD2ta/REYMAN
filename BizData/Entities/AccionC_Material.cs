﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class AccionC_Material
    {
        public int AccionC_MaterialID { get; set; }
        public Decimal? PrecioCUP { get; set; }
        public Decimal? PrecioCUC { get; set; }

        public virtual AccionConstructiva AccionConstructiva { get; set; }
        public virtual Material Material { get; set; }
    }
}