using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public enum EstadoPlan
    {
        planificación,
        aprovado,
        ejecución,
        ejecutado
    }

    public class Plan
    {
        public int PlanID { get; set; }
        public Decimal Presupuesto { get; set; }
        public int Año { get; set; }
        public string TipoPlan { get; set; }
        public EstadoPlan Estado { get; set; }

        public virtual ICollection<AccionConstructiva> AccionesConstructivas { get; set; }
    }
}
