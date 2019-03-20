using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class PlanCommand : PlanViewModel
    {
        public Plan ToPlan()
        {
            return new Plan
            {
                Presupuesto = Presupuesto,
                Año = Año,
                TipoPlan = TipoPlan
            };
        }
    }
}
