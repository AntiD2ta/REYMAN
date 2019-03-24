using BizData.Entities;
using BizDbAccess.Authentication;
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
        private readonly UnidadOrganizativaDbAccess _unidadOrganizativaDbAccess;
        private readonly IUnitOfWork _context;

        public InvestorServices(IUnitOfWork context)
        {
            _context = context;
            _runnerPlan = new RunnerWriteDb<PlanCommand, Plan>(
                new RegisterPlanAction(new PlanDbAccess(_context)), _context);
            _runnerInmueble = new RunnerWriteDb<InmuebleCommand, Inmueble>(
                new RegisterInmuebleAction(new InmuebleDbAccess(_context)), _context);
            _planDbAccess = new PlanDbAccess(_context);
            _unidadOrganizativaDbAccess = new UnidadOrganizativaDbAccess(_context);
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

        public Plan UpdatePlan(Plan entity, Plan toUpd)
        {
            var plan = _planDbAccess.Update(entity, toUpd);
            _context.Commit();
            return plan;
        }

        public long RegisterInmueble(InmuebleCommand cmd, string nombreUO)
        {
            var uo = _unidadOrganizativaDbAccess.GetUO(nombreUO);
            cmd.UO = uo;

            var inm = _runnerInmueble.RunAction(cmd);

            if (_runnerInmueble.HasErrors) return 0;

            return inm.InmuebleID;
        }
    }
}