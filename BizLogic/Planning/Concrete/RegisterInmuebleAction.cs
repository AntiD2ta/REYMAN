using BizData.Entities;
using BizDbAccess.User;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterInmuebleAction : BizActionErrors, IBizAction<InmuebleCommand, Inmueble>
    {
        private readonly InmuebleDbAccess _dbAccess;

        public RegisterInmuebleAction(InmuebleDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Inmueble Action(InmuebleCommand dto)
        {
            var inm = dto.ToInmueble();

            try
            {
                _dbAccess.GetInmueble(inm.UO, inm.Direccion);
            }
            catch
            {
                AddError($"Ya existe ese inmueble en {inm.UO.Nombre}");
            }

            if (!HasErrors)
                _dbAccess.Add(inm);

            return HasErrors ? null : inm;
        }
    }
}
