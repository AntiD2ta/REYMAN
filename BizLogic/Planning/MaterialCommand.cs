using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class MaterialCommand
    {
        public AccionC_Material AC_M { get; set; }
        public Material Material { get; set; }

        public MaterialCommand(AccionC_Material aC_M, Material material)
        {
            AC_M = aC_M;
            Material = material;
        }

        //public Material ToMaterial(string nombre, int unidadMedida)
        //{
        //    return new Material()
        //    {
        //        Nombre = nombre,
        //        UnidadMedida = unidadMedida
        //    };
        //}
    }
}
