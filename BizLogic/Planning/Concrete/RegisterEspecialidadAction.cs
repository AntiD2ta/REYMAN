using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterEspecialidadAction : BizActionErrors, IBizAction<EspecialidadCommand, Especialidad>
    {
        private readonly EspecialidadDbAccess _dbAccess;

        public RegisterEspecialidadAction(EspecialidadDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Especialidad Action(EspecialidadCommand dto)
        {
            var esp = dto.ToEspecialida();

            if (_dbAccess.GetEspecialidad(esp.Tipo) != null)
                AddError("Ya existe esa especialidad");

            if (!HasErrors)
                _dbAccess.Add(esp);

            return HasErrors ? null : esp;
        }
    }
}
