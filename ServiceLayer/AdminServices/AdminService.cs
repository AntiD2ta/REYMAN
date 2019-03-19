using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizLogic.Administration;
using BizLogic.Administration.Concrete;
using BizLogic.Authentication.Concrete;
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
        private readonly ProvinciaDbAccess _provDbAccess;

        public AdminService(IUnitOfWork context)
        {
            _runnerUO = new RunnerWriteDb<UOCommand, UnidadOrganizativa>(
                new RegisterUOAction(new UnidadOrganizativaDbAccess(context)), context);
            _runnerProv = new RunnerWriteDb<ProvinciaViewModel, Provincia>(
                new RegisterProvAction(new ProvinciaDbAccess(context)), context);
            _provDbAccess = new ProvinciaDbAccess(context);
        }

        public long RegisterUO(UOViewModel vm)
        {
            var provs = GetProvincias();

            var uo = _runnerUO.RunAction(new UOCommand(provs));

            if (_runnerUO.HasErrors) return 0;

            return uo.UnidadOrganizativaID;
        }

        public long RegisterProvincia(ProvinciaViewModel vm)
        {
            var prov = _runnerProv.RunAction(vm);

            if (_runnerProv.HasErrors) return 0;

            return prov.ProvinciaID;
        }

        public long DeleteProvincia(Provincia prov)
        {
            _provDbAccess.Delete(prov);
            return prov.ProvinciaID;
        }

        public IEnumerable<Provincia> GetProvincias()
        {
            return _provDbAccess.GetAll();
        }
    }
}
