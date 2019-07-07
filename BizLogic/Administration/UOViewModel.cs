using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class UOViewModel
    {
        [Required(ErrorMessage = "El nombre de la Unidad Organizativa es necesario."]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Provincia es necesaria."]
        [StringLength(100)]
        public string Provincia { get; set; }

        public int Id { get; set; }

        public string button { get; set; }

        public UnidadOrganizativa ToUO()
        {
            return new UnidadOrganizativa
            {
                Nombre = Nombre
            };
        }
    }
}
