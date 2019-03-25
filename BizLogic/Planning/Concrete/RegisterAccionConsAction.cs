using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterAccionConsAction : BizActionErrors, IBizAction<AccionConsCommand, AccionConstructiva>
    {
        private readonly AccionConstructivaDbAccess _dbAccess;

        public RegisterAccionConsAction(AccionConstructivaDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public AccionConstructiva Action(AccionConsCommand dto)
        {
            var ac = dto.ToAC();

            try
            {
                _dbAccess.GetAccionCons(ac.Nombre, ac.ObjetoObra);
            }
            catch (InvalidOperationException)
            {
                AddError($"Una acción constructiva con nombre {ac.Nombre} ya existe en el" +
                    $"objeto de obra {ac.ObjetoObra.Nombre}");
            }

            if (!HasErrors)
                _dbAccess.Add(ac);

            return HasErrors ? null : ac;
        }
    }
}
