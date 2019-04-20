﻿using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using BizDbAccess.Utils;
using BizLogic.Administration;
using BizLogic.Administration.Concrete;
using BizLogic.Authentication.Concrete;
using BizLogic.Planning.Concrete;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices
{
    public class AdminService
    {
        private readonly RunnerWriteDb<UOCommand, UnidadOrganizativa> _runnerUO;
        private readonly RunnerWriteDb<ProvinciaViewModel, Provincia> _runnerProv;
        private readonly ProvinciaDbAccess _provDbAccess;
        private readonly UnidadOrganizativaDbAccess _unidadOrganizativaDbAccess;
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly GetterUtils _getterUtils;


        public AdminService(IUnitOfWork context, UserManager<Usuario> userManager, IGetterUtils getterUtils)
        {
            _runnerUO = new RunnerWriteDb<UOCommand, UnidadOrganizativa>(
                new RegisterUOAction(new UnidadOrganizativaDbAccess(context)), context);
            _runnerProv = new RunnerWriteDb<ProvinciaViewModel, Provincia>(
                new RegisterProvAction(new ProvinciaDbAccess(context)), context);
            _provDbAccess = new ProvinciaDbAccess(context);
            _unidadOrganizativaDbAccess = new UnidadOrganizativaDbAccess(context);
            _context = context;
            _userManager = userManager;
            _getterUtils = (GetterUtils)getterUtils;
        }

        public long RegisterUO(UOCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            var provs = GetProvincias();
            cmd.Provincias = provs;

            var uo = _runnerUO.RunAction(cmd);

            if (_runnerUO.HasErrors)
            {
                errors = _runnerUO.Errors;
                return -1;
            }

            errors = null;
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

        public async Task<(List<string> UserPendings, bool Provincias, bool UO)> FillNotificationsAsync()
        {   
            //check for pending users
            List<string> UserPendings = new List<string>();
            foreach (var user in _userManager.Users)
            {
                if ((await _userManager.GetClaimsAsync(user)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    UserPendings.Add(user.Email);
            }

            GetterAll getter = new GetterAll(_getterUtils, _context);

            //check for empty provincias
            bool Provincias = getter.GetAll("Provincia").Any();

            //check for empty UOs
            bool UO  = getter.GetAll("UnidadOrganizativa").Any();

            return (UserPendings, Provincias, UO);
        }

    }
}
