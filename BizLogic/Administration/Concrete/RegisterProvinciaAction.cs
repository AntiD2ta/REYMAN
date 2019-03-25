using BizData.Entities;
using BizDbAccess.Authentication;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterProvAction : BizActionErrors, IBizAction<ProvinciaViewModel, Provincia>
    {
        private readonly ProvinciaDbAccess _dbAccess;

        public RegisterProvAction(ProvinciaDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Provincia Action(ProvinciaViewModel dto)
        {
            var prov = new Provincia() { Nombre = dto.Nombre };

            foreach (var p in _dbAccess.GetAll())
            {
                if (p.Nombre == prov.Nombre)
                {
                    AddError($"La provincia {p.Nombre} ya existe");
                }
            }
            
            if (!HasErrors)
                _dbAccess.Add(prov);

            return HasErrors ? null : prov;
        }
    }
}
