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
            }
        }

        public IEnumerable<Material> GetAll()
        {
            return _context.Materiales;
        }

        public Material Update(Material entity, Material toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("El material que se desea modificar no existe");

            if (entity.AccionesConstructivas == null)
                entity.AccionesConstructivas = new List<AccionC_Material>();

            toUpd.AccionesConstructivas = toUpd.AccionesConstructivas == null ?
                                                entity.AccionesConstructivas :
                                                (toUpd.AccionesConstructivas.Concat(entity.AccionesConstructivas)).ToList();
            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.UnidadMedida = entity.UnidadMedida ?? toUpd.UnidadMedida;

            _context.Materiales.Update(toUpd);
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

        public Material GetMaterial(int MaterialID)
        {
            return _context.Materiales.Find(MaterialID);
        }
    }
}
