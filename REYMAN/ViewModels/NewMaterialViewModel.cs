using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REYMAN.ViewModels
{
    public class NewMaterialViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UnidadDeMedida { get; set; }
        public int Cantidad { get; set; }
        public double CUC { get; set; }
        public double CUP { get; set; }

        public IEnumerable<string> _Materiales { get; set; }
        public IEnumerable<UnidadMedida> Unidades { get; set; }
    }
}
