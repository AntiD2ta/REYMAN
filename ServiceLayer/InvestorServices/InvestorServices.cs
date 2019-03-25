using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.User;
using BizLogic.Planning;
using BizLogic.Planning.Concrete;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.InvestorServices
{
    public class InvestorServices
    {
        private readonly RunnerWriteDb<PlanCommand, Plan> _runnerPlan;
        private readonly RunnerWriteDb<InmuebleCommand, Inmueble> _runnerInmueble;
        private readonly PlanDbAccess _planDbAccess;
        private readonly IUnitOfWork _context;

        public InvestorServices(IUnitOfWork context)
        {
            _context = context;
            _runnerPlan = new RunnerWriteDb<PlanCommand, Plan>(
                new RegisterPlanAction(new PlanDbAccess(_context)), _context);
            _planDbAccess = new PlanDbAccess(_context);

        }

        public long RegisterPlan(PlanCommand cmd)
        {
            var plan = _runnerPlan.RunAction(cmd);

            if (_runnerPlan.HasErrors) return 0;

            return plan.PlanID;
        }

        public Plan GetPlan(int año, string tipo)
        {
            return _planDbAccess.GetPlan(año, tipo);
        }

        public void UpdatePlan(Plan plan, PlanCommand cmd)
        {
            var upd = cmd.ToPlan();
            upd.PlanID = plan.PlanID;

            _planDbAccess.Update(upd);
            _context.Commit();
        }

        //public long RegisterInmueble(InmuebleCommand cmd)
        //{
        //    var inm = cmd.ToInmueble();

            
        //}
    }
}