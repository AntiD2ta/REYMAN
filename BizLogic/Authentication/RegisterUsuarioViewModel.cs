using BizData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace BizLogic.Authentication
{
    public class RegisterUsuarioViewModel
    {
        [Required(ErrorMessage = "Complete el campo de Primer Nombre correctamente.")]
        [StringLength(100), DisplayName("Primer Nombre")]
        public string FirstName { get; set; }

        [StringLength(100), DisplayName("Segundo Nombre(Opcional)")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Complete el campo de Primer Apellido correctamente.")]
        [StringLength(100), DisplayName("Primer Apellido")]
        public string FirstLastName { get; set; }

        [Required(ErrorMessage = "Complete el campo de Segundo Apellido correctamente.")]
        [StringLength(100), DisplayName("Segundo Apellido")]
        public string SecondLastName { get; set; }

        [Required(ErrorMessage = "Complete el campo de Email correctamente.")]
        [StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Complete el campo de Contraseña correctamente.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [DisplayName("Confirme su contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Su contraseña y la confirmación no son iguales")]
        public string ConfirmPassword { get; set; }

        public string EditEmail { get; set; }

        public int UO { get; set; }

        public IEnumerable<UnidadOrganizativa> UOs { get; set; }

        public string Claim { get; set; }
    }
}
