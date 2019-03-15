using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Usuario : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }

        public UnidadOrganizativa UnidadOrganizativa { get; set; }
    }
}
