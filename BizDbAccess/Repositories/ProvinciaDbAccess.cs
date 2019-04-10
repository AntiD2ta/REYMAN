using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Authentication
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore.
    /// </summary>
    public class ProvinciaDbAccess : IEntityDbAccess<Provincia>
    {
        private readonly EfCoreContext _context;

        public ProvinciaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Provincia entity)
        {
            _context.Provincias.Add(entity);
        }

        public void Delete(Provincia entity)
        {
            _context.Provincias.Remove(entity);
        }

        public IEnumerable<Provincia> GetAll()
        {
            return _context.Provincias.OrderBy(i => i.Nombre);
        }

        public Provincia Update(Provincia entity, Provincia toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("No existe la provincia que se desea actualizar");

            toUpd.Nombre = entity.Nombre;
            _context.Provincias.Update(toUpd);
            return toUpd;
        }
    }
}
