using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.User;
using BizLogic.Planning;
using BizLogic.Planning.Concrete;
using ServiceLayer.BizRunners;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.InvestorServices
{
    public class InvestorServices
    {
        private readonly RunnerWriteDb<PlanCommand, Plan> _runnerPlan;
        private readonly RunnerWriteDb<InmuebleCommand, Inmueble> _runnerInmueble;
        private readonly RunnerWriteDb<ObjObraCommand, ObjetoObra> _runnerObjObra;

        private readonly PlanDbAccess _planDbAccess;
        private readonly InmuebleDbAccess _inmuebleDbAccess;
        private readonly ObjetoObraDbAccess _objetoObraDbAccess;
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
            _inmuebleDbAccess = new InmuebleDbAccess(_context);
            _objetoObraDbAccess = new ObjetoObraDbAccess(_context);
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

        public bool CheckInmuebles(out IEnumerable<Inmueble> inmuebles)
        {
            inmuebles = _inmuebleDbAccess.GetAll();
            return inmuebles.Any();
        }

        public long RegisterInmueble(InmuebleCommand cmd, string nombreUO)
        {
            var uo = _unidadOrganizativaDbAccess.GetUO(nombreUO);
            cmd.UO = uo;

            var inm = _runnerInmueble.RunAction(cmd);

            if (_runnerInmueble.HasErrors) return 0;

            return inm.InmuebleID;
        }

        public Inmueble UpdateInmueble(Inmueble entity, Inmueble toUpd)
        {
            if (entity.Direccion != null && entity.Direccion != toUpd.Direccion &&
                _inmuebleDbAccess.GetInmueble(entity.UO, entity.Direccion) != null)
            {
                throw new Exception($"Ya existe un Inmueble con direccion {entity.Direccion}");
            }

            var inmueble = _inmuebleDbAccess.Update(entity, toUpd);
            _context.Commit();
            return inmueble;
        }

        public bool CheckForInmuebles(string nombreUO)
        {
            var uo = _unidadOrganizativaDbAccess.GetUO(nombreUO);
            return uo.Inmuebles.Any();
        }

        public Inmueble AddObjObraToInm(Inmueble entity, IEnumerable<ObjetoObra> objsObra)
        {
            _inmuebleDbAccess.AddObjObra(ref entity, objsObra);
            _context.Commit();
            return entity;
        }

        public long RegisterObjObra(ObjObraCommand vm, string dirInmueble)
        {
            vm.Inmueble = _inmuebleDbAccess.GetInmueble(_unidadOrganizativaDbAccess.GetUO(vm.nombreUO),
                            dirInmueble);

            var objObra = _runnerObjObra.RunAction(vm);

            if (_runnerObjObra.HasErrors) return 0;

            return objObra.ObjetoObraID;
        }

        public ObjetoObra UpdateObjetoObra(ObjetoObra entity, ObjetoObra toUpd)
        {
            if (entity.Nombre != null && entity.Nombre != toUpd.Nombre &&
                _objetoObraDbAccess.GetObjObra(entity.Nombre, entity.Inmueble.Direccion,
                entity.Inmueble.UO.Nombre) != null)
            {
                throw new Exception($"Ya existe un Objeto de Obra con nombre {entity.Nombre}");
            }

            var objObra = _objetoObraDbAccess.Update(entity, toUpd);
            _context.Commit();
            return objObra;
        }
    }
}