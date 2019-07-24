using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Email es necesario.")]
        [StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es incorrecta."), StringLength(100)]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
