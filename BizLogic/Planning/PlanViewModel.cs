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

        [Required]
        public int Año { get; set; }

        [Required] 
        public string TipoPlan { get; set; }
    }
}
