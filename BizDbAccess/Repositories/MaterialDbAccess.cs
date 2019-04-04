using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.User
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore.
    /// </summary>
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

        public Material GetMaterial(string nombre, string unidadMedida)
        {
            return _context.Materiales.Where(m => m.Nombre == nombre &&
                                                  m.UnidadMedida.Nombre == unidadMedida)
                                                  .SingleOrDefault();
        }

        public Material GetMaterial(string nombre)
        {
            return _context.Materiales.Where(m => m.Nombre == nombre).First();
        }

        public Material GetMaterial(long MaterialID)
        {
            return _context.Materiales.Find(MaterialID);
        }
    }
}
