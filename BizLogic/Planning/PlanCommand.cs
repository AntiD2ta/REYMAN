using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class PlanCommand : PlanViewModel
    {
        public string button { get; set; }
        public int PlanID { get; set; }
        public UnidadOrganizativa UO { get; set; }

        public Plan ToPlan()
        {
            return new Plan
            {
                Presupuesto = Presupuesto,
                Año = Año,
                TipoPlan = TipoPlan,
                UnidadOrganizativa = UO,
                AccionesConstructivas = new List<AccionConstructiva>()
            };
        }
        public void Set(Plan plan)
        {
            Presupuesto = plan.Presupuesto;
            Año = plan.Año;
            TipoPlan = plan.TipoPlan;
            PlanID = plan.PlanID;
        }
    }
}
