using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class ManoObra
    {
        public int ManoObraID { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public double Precio { get; set; }

        public virtual AccionConstructiva AccionConstructiva { get; set; }
    }
}
