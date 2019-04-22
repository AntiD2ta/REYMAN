using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class EspecialidadCommand : EspecialidadViewModel
    {
        public Especialidad ToEspecialida()
        {
            return new Especialidad() { Tipo = Nombre };
        }
    }
}
