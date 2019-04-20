using BizData.Entities;
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
        
        public string Direccion { get; set; }
        public IEnumerable<string> Inmuebles { get; set; }
    }
}
