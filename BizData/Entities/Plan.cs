using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Plan
    {
        public int PlanID { get; set; }
        public double Presupuesto { get; set; }
        public int Año { get; set; }
        public string TipoPlan { get; set; }

        public virtual ICollection<AccionConstructiva> AccionesConstructivas { get; set; }
    }
}
