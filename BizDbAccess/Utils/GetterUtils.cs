using BizDbAccess.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BizDbAccess.Utils
{
    /// <summary>
    /// Helper class, contains GetterAll static's utilities. Modify if
    /// a new repository arises, or change entities proyect.
    /// </summary>
    public class GetterUtils : IGetterUtils
    {
        public Dictionary<string, string> ReposNames { get; }
        public AssemblyName targetAssembly { get; }

        public GetterUtils()
        {
            ReposNames = new Dictionary<string, string>
            {
                { "AccionConstructiva", "AccionConstructivaDbAccess" },
                { "AccionC_Material", "AccionC_MaterialDbAccess" },
                { "Especialidad", "EspecialidadDbAccess" },
                { "Inmueble", "InmuebleDbAccess" },
                { "Material", "MaterialDbAccess" },
                { "ManoObra", "ManoObraDbAccess" },
                { "ObjetoObra", "ObjetoObraDbAccess" },
                { "Usuario", "UserDbAccess" },
                { "UnidadMedida", "UnidadMedidaDbAccess" },
                { "UnidadOrganizativa", "UnidadOrganizativaDbAccess" },
                { "Provincia", "ProvinciaDbAccess"},
                { "Plan", "PlanDbAccess" }
            };

            targetAssembly = new AssemblyName("BizData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

    }
}
