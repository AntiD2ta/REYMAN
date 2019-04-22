using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning
{
    public class UnidadMedidaCommand : UnidadMedidaViewModel
    {
        public UnidadMedida ToUnidadMedida()
        {
            return new UnidadMedida { Nombre = Nombre };
        }
    }
}
