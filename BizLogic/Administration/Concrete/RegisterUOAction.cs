using BizData.Entities;
using BizDbAccess.Authentication;
using BizLogic.GenericInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.Administration.Concrete
{
    public class RegisterUOAction : BizActionErrors, IBizAction<UOCommand, UnidadOrganizativa>
    {
        private readonly UnidadOrganizativaDbAccess _dbAccess;

        public RegisterUOAction(UnidadOrganizativaDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public UnidadOrganizativa Action(UOCommand dto)
        {
            Provincia prov = new Provincia();

            foreach (var p in dto.Provincias)
            {
                if (p.Nombre == dto.Provincia)
                {
                    prov = p;
                    break;
                }
            }

            var uo = new UnidadOrganizativa()
            {
                Nombre = dto.Nombre,
                Provincia = prov
            };

            foreach (var u in _dbAccess.GetAll())
            {
                if (uo.Nombre == u.Nombre)
                {
                    AddError($"Ya existe una unidad organizativa con {uo.Nombre}");
                }
            }

            if (!HasErrors)
                _dbAccess.Add(uo);

            return HasErrors ? null : uo;
        }
    }
}
