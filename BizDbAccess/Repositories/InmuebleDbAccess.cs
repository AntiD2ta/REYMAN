using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.User
{
    public class InmuebleDbAccess : IEntityDbAccess<Inmueble>
    {
        public readonly EfCoreContext _context;

        public InmuebleDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Inmueble entity)
        {
            _context.Inmuebles.Add(entity);
        }

        public void Delete(Inmueble entity)
        {
            if (_context.Inmuebles.Find(entity.InmuebleID) != null)
            {
                _context.Inmuebles.Remove(entity);
                _context.Commit();
            }
        }

        public IEnumerable<Inmueble> GetAll()
        {
            return _context.Inmuebles;
        }

        public void Update(Inmueble entity)
        {
            if (_context.Inmuebles.Find(entity.InmuebleID) != null)
            {
                _context.Inmuebles.Update(entity);
                _context.Commit();
            }
        }
    }
}
