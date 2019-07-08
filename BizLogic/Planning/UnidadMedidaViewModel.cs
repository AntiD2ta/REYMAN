using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class UnidadMedidaViewModel
    {
        [Required(ErrorMessage = "El Nombre de la Unidad de Medida es necesario.")]
        public string Nombre { get; set; }
    }
}
