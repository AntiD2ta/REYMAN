using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class ProvinciaViewModel
    {
        [Required(ErrorMessage = "El Nombre de la Provincia de llenarse"), StringLength(100, MinimumLength = 1)]
        public string Nombre { get; set; }
        public int Id { get; set; }
        public string NombreBorrar { get; set; }
        public string button { get; set; }
        public IEnumerable<object> GetProvincia { get; set; }

        public Provincia ToProvincia()
        {
            return new Provincia
            {
                Nombre = Nombre
            };
        }
    }
}
