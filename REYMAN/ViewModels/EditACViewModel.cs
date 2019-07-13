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
        public string Especialidad { get; set; }
        public decimal? CUC { get; set; }
        public decimal? CUP { get; set; }
        public int Cantidad { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Inmueble { get; set; }
        public string ObjetoDeObra { get; set; }

        public IEnumerable<Especialidad> Especialidades { get; set; }
        public IEnumerable<UnidadMedida> Unidades { get; set; }
        public IEnumerable<Inmueble> Inmuebles { get; set; }
    }
}
