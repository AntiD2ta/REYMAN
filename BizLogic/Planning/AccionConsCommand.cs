using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionConsCommand : AccionConsViewModel
    {
        public Plan Plan { get; set; }
        public Especialidad Especialidad { get; set; }
        public ObjetoObra ObjetoObra { get; set; }

        public AccionConstructiva ToAC()
        {
            return new AccionConstructiva()
            {
                Nombre = Nombre,
                Especialidad = Especialidad,
                Plan = Plan,
                ObjetoObra = ObjetoObra
            };
        }
    }
}
