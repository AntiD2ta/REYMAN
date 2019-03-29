using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class UnidadMedidaDbAccess : IEntityDbAccess<UnidadMedida>
    {
        private readonly EfCoreContext _context;

        public UnidadMedidaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(UnidadMedida entity)
        {
            _context.UnidadesMedida.Add(entity);
        }

        public void Delete(UnidadMedida entity)
        {
            _context.UnidadesMedida.Remove(entity);
        }

        public IEnumerable<UnidadMedida> GetAll() => _context.UnidadesMedida;

        public UnidadMedida Update(UnidadMedida entity, UnidadMedida toUpd)
        {
            throw new NotImplementedException();
        }

        public UnidadMedida GetUM(string nombre)
        {
            return _context.UnidadesMedida.Where(um => um.Nombre == nombre).SingleOrDefault();
        }
    }
}
