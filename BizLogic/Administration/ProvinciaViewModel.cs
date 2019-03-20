using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class ProvinciaViewModel
    {
        [Required, StringLength(100, MinimumLength = 1)]
        public string Nombre { get; set; }
        public string NombreBorrar { get; set; }
        public string button { get; set; }
    }
}
