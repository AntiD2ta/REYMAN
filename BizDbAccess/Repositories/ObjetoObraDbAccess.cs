using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.User
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore.
    /// </summary>
    public class ObjetoObraDbAccess : IEntityDbAccess<ObjetoObra>
    {
        public readonly EfCoreContext _context;

        public ObjetoObraDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(ObjetoObra entity)
        {
            _context.ObjetosObra.Add(entity);
        }

        public void Delete(ObjetoObra entity)
        {
            if (_context.ObjetosObra.Find(entity.ObjetoObraID) != null)
            {
                _context.ObjetosObra.Remove(entity);
            }
        }

        public IEnumerable<ObjetoObra> GetAll()
        {
            return _context.ObjetosObra;
        }

        public ObjetoObra Update(ObjetoObra entity, ObjetoObra toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe el inmueble que se quiere actualizar");

            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.Inmueble = entity.Inmueble ?? toUpd.Inmueble;
            toUpd.AccionesConstructivas = entity.AccionesConstructivas ?? toUpd.AccionesConstructivas;

            _context.ObjetosObra.Update(toUpd);
            return toUpd;
        }

        public ObjetoObra GetObjObra(string nombre, string dirInmueble, string nombreUO)
        {
            return _context.ObjetosObra.Where(obj => obj.Nombre == nombre &&
                    obj.Inmueble.Direccion == dirInmueble &&
                    obj.Inmueble.UnidadOrganizativa.Nombre == nombreUO).SingleOrDefault();
        }

        public ObjetoObra GetObjObra(int id)
        {
            return _context.ObjetosObra.Find(id);
        }
    }
}
