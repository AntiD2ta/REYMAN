using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class ObjObraCommand : ObjObraViewModel
    {
        public Inmueble Inmueble { get; set; }
        public string nombreUO { get; set; }

        public ObjetoObra ToObjObra()
        {
            return new ObjetoObra()
            {
                Nombre = Nombre,
                Inmueble = Inmueble
            };
        }
    }
}
