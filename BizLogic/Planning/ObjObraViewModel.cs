using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class ObjObraViewModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
