using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Authentication
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore.
    /// </summary>
    public class UnidadOrganizativaDbAccess : IEntityDbAccess<UnidadOrganizativa>
    {
        public readonly EfCoreContext _context;

        public UnidadOrganizativaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(UnidadOrganizativa entity)
        {
            _context.UnidadesOrganizativas.Add(entity);
        }

        public void Delete(UnidadOrganizativa entity)
        {
            if (_context.UnidadesOrganizativas.Find(entity.UnidadOrganizativaID) != null)
            {
                _context.UnidadesOrganizativas.Remove(entity);
            }
        }

        public IEnumerable<UnidadOrganizativa> GetAll()
        {
            return _context.UnidadesOrganizativas;
        }

        public UnidadOrganizativa Update(UnidadOrganizativa entity, UnidadOrganizativa toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("No existe la unidad organizativa que se desea eliminar.");

            if (entity.Inmuebles == null)
                entity.Inmuebles = new List<Inmueble>();
            if (entity.Usuarios == null)
                entity.Usuarios = new List<Usuario>();

            toUpd.Inmuebles = toUpd.Inmuebles == null ? entity.Inmuebles : (toUpd.Inmuebles.Concat(entity.Inmuebles)).ToList();
            toUpd.Usuarios = toUpd.Usuarios == null ? entity.Usuarios : (toUpd.Usuarios.Concat(entity.Usuarios)).ToList();
            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.Provincia = entity.Provincia ?? toUpd.Provincia;

            _context.UnidadesOrganizativas.Update(toUpd);
            return toUpd;
        }

        public UnidadOrganizativa GetUO(string nombreUO)
        {
            return _context.UnidadesOrganizativas.Where(uo => uo.Nombre == nombreUO).Single();
        }
    }
}
