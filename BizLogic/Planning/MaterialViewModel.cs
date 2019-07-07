using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class MaterialViewModel
    {
        [Required(ErrorMessage = "El Nombre del Material es necesario.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Unidad de Medida del Material es necesaria.")]
        public string UnidadMedida { get; set; }

        public string Button { get; set; }
        public int MaterialId { get; set; }
        public IEnumerable<UnidadMedida> UnidadesMedida { get; set; }
    }
}
