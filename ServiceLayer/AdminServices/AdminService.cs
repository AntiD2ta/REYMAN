using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using BizLogic.Administration;
using BizLogic.Administration.Concrete;
using BizLogic.Authentication.Concrete;
using BizLogic.Planning.Concrete;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.AdminServices
{
    public class AdminService
    {
        private readonly RunnerWriteDb<UOCommand, UnidadOrganizativa> _runnerUO;
        private readonly RunnerWriteDb<ProvinciaViewModel, Provincia> _runnerProv;
        private readonly RunnerWriteDb<UnidadOrganizativa, PlanActual> _runnerPlanAct;
        private readonly ProvinciaDbAccess _provDbAccess;
        private readonly UnidadOrganizativaDbAccess _unidadOrganizativaDbAccess;
        private readonly IUnitOfWork _context;

        public AdminService(IUnitOfWork context)
        {
            _runnerUO = new RunnerWriteDb<UOCommand, UnidadOrganizativa>(
                new RegisterUOAction(new UnidadOrganizativaDbAccess(context)), context);
            _runnerProv = new RunnerWriteDb<ProvinciaViewModel, Provincia>(
                new RegisterProvAction(new ProvinciaDbAccess(context)), context);
            _runnerPlanAct = new RunnerWriteDb<UnidadOrganizativa, PlanActual>(
                new RegisterPlanActualAction(new PlanActualDbAccess(context)), context);
            _provDbAccess = new ProvinciaDbAccess(context);
            _unidadOrganizativaDbAccess = new UnidadOrganizativaDbAccess(context);
            _context = context;
        }

        //TODO: RegisterUO debe tener un out errors.
        public long RegisterUO(UOCommand cmd)
        {
            var provs = GetProvincias();
            cmd.Provincias = provs;

            var uo = _runnerUO.RunAction(cmd);

            if (_runnerUO.HasErrors) return -1;

            _runnerPlanAct.RunAction(uo);

            if (_runnerPlanAct.HasErrors) return -1;

            return uo.UnidadOrganizativaID;
        }

        public UnidadOrganizativa UpdateUO(UnidadOrganizativa entity, UnidadOrganizativa toUpd)
        {
            var uo = _unidadOrganizativaDbAccess.Update(entity, toUpd);
            _context.Commit();
            return uo;
        }

        public void DeleteUO(UnidadOrganizativa entity)
        {
            _unidadOrganizativaDbAccess.Delete(entity);
            _context.Commit();
        }
        
        //Devolver errores en RegisterProvinica
        public long RegisterProvincia(ProvinciaViewModel vm)
        {
            var prov = _runnerProv.RunAction(vm);

            if (_runnerProv.HasErrors) return 0;

            return prov.ProvinciaID;
        }

        public Provincia UpdateProvincia(Provincia entity, Provincia toUpd)
        {
            var prov = _provDbAccess.Update(entity, toUpd);
            _context.Commit();
            return prov;
        }

        public long DeleteProvincia(ProvinciaViewModel vm)
        {
            Provincia prov = new Provincia();
            prov.Nombre = vm.Nombre;
            _provDbAccess.Delete(prov);
            _context.Commit();
            return prov.ProvinciaID;
        }

        public long DeleteProvincia(Provincia prov)
        {
            _provDbAccess.Delete(prov);
            _context.Commit();
            return prov.ProvinciaID;
        }

        public IEnumerable<Provincia> GetProvincias()
        {  
            return _provDbAccess.GetAll();
        }

    }
}
