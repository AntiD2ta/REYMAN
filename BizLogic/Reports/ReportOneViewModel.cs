using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Reports
{
    public class ReportOneViewModel
    {
        public ReportOne Report { get; set; }
        public int Año { get; set; }
        public string TipoPlan { get; set; }
        public List<string> TiposPlan = new List<string>() { "Reparación", "Mantenimiento" };
    }
}
