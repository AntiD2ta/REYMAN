using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class EspViewModel
    {
        public IEnumerable<Especialidad> Especialidad { get; set; }
        public int id { get; set; }
    }
}
