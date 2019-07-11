using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REYMAN.ViewModels
{
    public class EditACViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Especialidad { get; set; }
        public double CUC { get; set; }
        public double CUP { get; set; }
        public int Cantidad { get; set; }
        public int UnidadDeMedida { get; set; }
        public int Inmueble { get; set; }
        public int ObjetoDeObra { get; set; }

        public IEnumerable<Especialidad> Especialidades { get; set; }
        public IEnumerable<UnidadMedida> Unidades { get; set; }
        public IEnumerable<Inmueble> Inmuebles { get; set; }
    }
}
