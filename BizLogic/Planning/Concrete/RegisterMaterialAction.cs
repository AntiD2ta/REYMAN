using BizData.Entities;
using BizDbAccess.User;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    public class RegisterMaterialAction : BizActionErrors, IBizAction<MaterialCommand, Material>
    {
        private readonly MaterialDbAccess _dbAccess;

        public RegisterMaterialAction(MaterialDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Material Action(MaterialCommand dto)
        {
            var mat = dto.Material;

           if(_dbAccess.GetMaterial(mat.Nombre, mat.UnidadMedida.Nombre) != null)
               AddError("Ya existe ese material");

            //if (dto.AC_M.AccionConstructiva.Materiales.Where(x => x.Material.Nombre == mat.Nombre &&
            //    x.Material.UnidadMedida.Nombre.ToLower() == mat.UnidadMedida.Nombre.ToLower()).Any())
            //{
            //    AddError($"Ya existe ese material en {dto.AC_M.AccionConstructiva.Nombre}");
            //}

            //dto.AC_M.Material = mat;
            //mat.AccionesConstructivas = new List<AccionC_Material>() { dto.AC_M };

            if (!HasErrors)
                _dbAccess.Add(mat);

            return HasErrors ? null : mat;
        }
    }
}
