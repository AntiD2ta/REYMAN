using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class EspecialidadViewModel
    {
        [Required(ErrorMessage = "El Nombre de la Especialidad es necesario.")]
        public string Nombre { get; set; }
    }
}
