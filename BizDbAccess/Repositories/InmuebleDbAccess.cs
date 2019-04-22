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
            }
        }

        public Inmueble GetInmueble(UnidadOrganizativa uo, string direccion)
        {
            return _context.Inmuebles.Where(i => i.UO == uo && i.Direccion == direccion).SingleOrDefault();
        }

        public IEnumerable<Inmueble> GetAll()
        {
            return _context.Inmuebles;
        }

        public Inmueble Update(Inmueble entity, Inmueble toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe el inmueble que se quiere actualizar");

            toUpd.Direccion = entity.Direccion ?? toUpd.Direccion;
            toUpd.ObjetosDeObra = entity.ObjetosDeObra ?? toUpd.ObjetosDeObra;
 
            _context.Inmuebles.Update(toUpd);

            return toUpd;
        }

        public void AddObjObra(ref Inmueble entity, IEnumerable<ObjetoObra> objsObra)
        {
            entity.ObjetosDeObra = entity.ObjetosDeObra.Concat(objsObra).ToList();
            _context.Inmuebles.Update(entity);
        }
    }
}
