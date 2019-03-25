using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.User
{
    public class MaterialDbAccess : IEntityDbAccess<Material>
    {
        public readonly EfCoreContext _context;

        public MaterialDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Material entity)
        {
            _context.Materiales.Add(entity);
        }

        public void Delete(Material entity)
        {
            if (_context.Materiales.Find(entity.MaterialID) != null)
            {
                _context.Materiales.Remove(entity);
                _context.Commit();
            }
        }

        public IEnumerable<Material> GetAll()
        {
            return _context.Materiales;
        }

        public Material Update(Material entity, Material toUpd)
        {
            if (_context.Materiales.Find(entity.MaterialID) != null)
            {
                _context.Materiales.Update(entity);
                _context.Commit();
            }
            return toUpd;
        }
    }
}
