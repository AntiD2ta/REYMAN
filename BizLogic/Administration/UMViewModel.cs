using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class UMViewModel
    {
        public IEnumerable<UnidadMedida> UnidadMedida { get; set; }
        public int id { get; set; }
    }
}
