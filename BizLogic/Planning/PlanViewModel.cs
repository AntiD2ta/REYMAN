using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Planning
{
    public class PlanViewModel
    {
        public Decimal Presupuesto { get; set; }

        [Required(ErrorMessage = "El Año del Plan es necesario.")]
        public int Año { get; set; }

        [Required(ErrorMessage = "El Tipo del Plan es necesario.")] 
        public string TipoPlan { get; set; }
    }
}
