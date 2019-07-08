using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var res = _dbAccess.GetAccionCons(ac.Nombre, ac.ObjetoObra, dto.Plan.TipoPlan);
                if (res != null)
                    AddError($"Una acción constructiva con nombre {ac.Nombre} ya existe en el" +
                        $"objeto de obra {ac.ObjetoObra.Nombre}");
            }
            catch (InvalidOperationException)
            {
                AddError($"Una acción constructiva con nombre {ac.Nombre} ya existe en el" +
                    $"objeto de obra {ac.ObjetoObra.Nombre}");
            }

            if (!HasErrors)
            {
                _dbAccess.Add(ac);
                if (dto.MaterialPrecio.Count > 0)
                {
                    var data = dto.MaterialPrecio.Select(t => new AccionC_Material()
                    {
                        AccionConstructiva = ac,
                        Material = t.material,
                        PrecioCUP = t.precioCUP,
                        PrecioCUC = t.precioCUC
                    });
                    ac.Materiales = data.ToList();
                }
            }

            return HasErrors ? null : ac;
        }
    }
}
