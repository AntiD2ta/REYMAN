using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterUnidadMedidaAction : BizActionErrors, IBizAction<UnidadMedidaCommand, UnidadMedida>
    {
        private readonly UnidadMedidaDbAccess _dbAccess;

        public RegisterUnidadMedidaAction(UnidadMedidaDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public UnidadMedida Action(UnidadMedidaCommand dto)
        {
            var um = dto.ToUnidadMedida();

            if (_dbAccess.GetUM(um.Nombre) != null)
                AddError("Ya existe esa unidad de medida");
            
            if (!HasErrors)
                _dbAccess.Add(um);

            return HasErrors ? null : um;
        }
    }
}