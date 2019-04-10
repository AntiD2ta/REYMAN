using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class PlanActual
    {
        public int PlanActualID { get; set; }

        public virtual UnidadOrganizativa UnidadOrganizativa { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
