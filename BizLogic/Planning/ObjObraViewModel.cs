using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class ObjObraViewModel
    {
        [Required(ErrorMessage = "El Nombre del Objeto de Obra es necesario.")]
        public string Nombre { get; set; }
        
        public string Direccion { get; set; }

        public IEnumerable<string> Inmuebles { get; set; }

        public int Id { get; set; }

        public string button { get; set; }
    }
}
