using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class UOCommand : UOViewModel
    {
        public IEnumerable<Provincia> Provincias { get; set; }

        public string button { get; set; }

    }
}
