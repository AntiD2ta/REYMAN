using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public Vistas Vista { get; set; }
        public ICollection<Tuple<string,string>> Claims;
    }
    public enum Vistas{ 
    Plan,
    Materiales
    };
}
