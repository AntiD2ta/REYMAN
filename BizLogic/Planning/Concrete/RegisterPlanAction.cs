using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.User;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterPlanAction : BizActionErrors, IBizAction<PlanCommand, Plan>
    {
        private readonly PlanDbAccess _dbAccess;

        public RegisterPlanAction(PlanDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Plan Action(PlanCommand dto)
        {
            Plan plan = dto.ToPlan();

            if (plan.TipoPlan != "Reparación" &&
                plan.TipoPlan != "Mantenimiento")
            {
                AddError("El tipo de Plan debe ser Mantenimiento o Reparación");
            }

            try
            {
                _dbAccess.GetPlan(plan.Año, plan.TipoPlan);
            }
            catch
            {
                AddError("Ya existe ese plan");
            }

            if (!HasErrors)
                _dbAccess.Add(plan);

            return HasErrors ? null : plan;
        }
    }
}
