using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class AccionConsViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string TipoEspecialidad { get; set; }

        public int CantidadMO { get; set; }

        public Decimal Precio { get; set; }

        public string UM { get; set; }

        public IEnumerable<(string nameMaterial, string unidadMedida, Decimal? precio)> Materiales { get; set; }        

        public long PlanID { get; set; }

        public long ObjetoObraID { get; set; }
    }
}
