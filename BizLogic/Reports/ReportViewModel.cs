using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Reports
{
    public class ReportViewModel
    {
        public string Report { get; set; }
        public int Año { get; set; }
        public IEnumerable<UnidadOrganizativa> UOs { get; set; }
        public IEnumerable<Inmueble> INMs { get; set; }
        public bool IsReparacion { get; set; }
        public bool IsMatenimiento { get; set; }
    }
}
