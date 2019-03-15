using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Authentication
{
    public class LoginViewModel
    {
        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
