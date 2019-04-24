using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class InmuebleCommand : InmuebleViewModel
    {
        public UnidadOrganizativa UO { get; set; }

        public Inmueble ToInmueble()
        {
            return new Inmueble()
            {
                Direccion = Direccion,
                UnidadOrganizativa = UO
            };
        }
    }
}
