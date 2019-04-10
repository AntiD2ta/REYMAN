using System;
using System.Collections.Generic;
using System.Text;
using BizDbAccess.Repositories;
using BizData.Entities;
using BizLogic.GenericInterfaces;

namespace BizLogic.Planning.Concrete
{
    public class RegisterPlanActualAction : BizActionErrors, IBizAction<UnidadOrganizativa, PlanActual>
    {
        private readonly PlanActualDbAccess _dbAccess;

        public RegisterPlanActualAction(PlanActualDbAccess planActualDbAccess)
        {
            _dbAccess = planActualDbAccess;
        }

        public PlanActual Action(UnidadOrganizativa dto)
        {
            PlanActual pa1 = new PlanActual(), 
                       pa2 = new PlanActual();
 
            pa1.UnidadOrganizativa = dto;
            pa2.UnidadOrganizativa = dto;
            
            if (!HasErrors)
            {
                _dbAccess.Add(pa1);
                _dbAccess.Add(pa2);
            }

            return HasErrors ? null : pa1;
        }
    }
}
