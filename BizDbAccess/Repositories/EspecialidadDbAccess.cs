using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    /// <summary>
    /// Specific implementation of a repository for EntityFrameworkCore.
    /// </summary>
    public class EspecialidadDbAccess : IEntityDbAccess<Especialidad>
    {
        private readonly EfCoreContext _context;

        public EspecialidadDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Especialidad entity)
        {
            _context.Especialidades.Add(entity);
        }

        public void Delete(Especialidad entity)
        {
            _context.Especialidades.Remove(entity);
        }

        public IEnumerable<Especialidad> GetAll() => _context.Especialidades;

        public Especialidad Update(Especialidad entity, Especialidad toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe la Especialidad que se quiere modificar");

            toUpd.Tipo = entity.Tipo ?? toUpd.Tipo;

            _context.Especialidades.Update(toUpd);

            return toUpd;
        }

        public Especialidad GetEspecialidad(string tipo)
        {
            return _context.Especialidades.Where(e => e.Tipo == tipo).SingleOrDefault();
        }

        public Especialidad GetEspecialidad(int id)
        {
            return _context.Especialidades.Where(e => e.EspecialidadID == id).SingleOrDefault();
        }
    }
}
