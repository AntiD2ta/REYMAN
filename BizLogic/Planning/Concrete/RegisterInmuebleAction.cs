using BizData.Entities;
using BizDbAccess.User;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Planning.Concrete
{
    class RegisterInmuebleAction : BizActionErrors, IBizAction<InmuebleCommand, Inmueble>
    {
        private readonly InmuebleDbAccess _dbAccess;

        public RegisterInmuebleAction(InmuebleDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Inmueble Action(InmuebleCommand dto)
        {
            throw new NotImplementedException();
        }
    }
}
