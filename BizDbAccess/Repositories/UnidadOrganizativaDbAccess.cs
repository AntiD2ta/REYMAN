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
                _context.Commit();
            }
        }

        public IEnumerable<UnidadOrganizativa> GetAll()
        {
            return _context.UnidadesOrganizativas;
        }

        public UnidadOrganizativa Update(UnidadOrganizativa entity, UnidadOrganizativa toUpd)
        {
            if (_context.UnidadesOrganizativas.Find(entity.UnidadOrganizativaID) != null)
            {
                _context.UnidadesOrganizativas.Update(entity);
                _context.Commit();
            }
            return toUpd;
        }

        public UnidadOrganizativa GetUO(string nombreUO)
        {
            return _context.UnidadesOrganizativas.Where(uo => uo.Nombre == nombreUO).Single();
        }

        public void AddEspecialidad(ref UnidadOrganizativa entity, IEnumerable<Especialidad> especialidades)
        {
            entity.Especialidades = entity.Especialidades.Concat(especialidades).ToList();
            _context.Update(entity);
        }
    }
}
