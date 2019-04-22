using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Authentication
{
    public class RegisterUsuarioCommand : RegisterUsuarioViewModel
    {
        public Usuario ToUsuario()
        {
            return new Usuario
            {
                FirstName = FirstName,
                SecondName = SecondName,
                FirstLastName = FirstLastName,
                SecondLastName = SecondLastName,
                Email = Email,
                UserName = Email,
                
            };
        }

        public void SetViewModel(Usuario u)
        {
            FirstName = u.FirstName;
            SecondName = u.SecondName;
            FirstLastName = u.FirstLastName;
            SecondLastName = u.SecondLastName;
            Email = u.Email;
            EditEmail = u.Email;
            Password = "P9n$";
        }
    }
}
