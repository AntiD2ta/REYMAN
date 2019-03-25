using BizData.Entities;
using BizDbAccess.User;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterObjObraAction : BizActionErrors, IBizAction<ObjObraCommand, ObjetoObra>
    {
        private readonly ObjetoObraDbAccess _dbAccess;

        public RegisterObjObraAction(ObjetoObraDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public ObjetoObra Action(ObjObraCommand dto)
        {
            var objObra = dto.ToObjObra();

            try
            {
                _dbAccess.GetObjObra(objObra.Nombre, objObra.Inmueble.Direccion, dto.nombreUO);
            }
            catch(InvalidOperationException)
            {
                AddError($"Ya existe un objeto de obra {objObra.Nombre} en el inmueble {objObra.Inmueble}");
            };

            if (!HasErrors)
                _dbAccess.Add(objObra);

            return HasErrors ? null : objObra;
        }
    }
}
