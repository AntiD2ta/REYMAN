using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class PendingUsersViewModel
    {
        public IEnumerable<Usuario> Usuarios { get; set; }

        public string userID { get; set; }
    }
}
