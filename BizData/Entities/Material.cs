﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string Nombre { get; set; }

        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual ICollection<AccionC_Material> AccionesConstructivas { get; set; }

        public override bool Equals(object obj)
        {
            var material = obj as Material;
            return material != null &&
                   Nombre == material.Nombre &&
                   UnidadMedida.Nombre == material.UnidadMedida.Nombre;
        }
    }

    public class MaterialComparer : IEqualityComparer<Material>
    {
        public bool Equals(Material x, Material y) => x.Nombre == y.Nombre;

        public int GetHashCode(Material obj)
        {
            return obj.Nombre.GetHashCode();
        }
    }
}
