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
    public class AccionConstructivaDbAccess : IEntityDbAccess<AccionConstructiva>
    {
        private readonly EfCoreContext _context;

        public AccionConstructivaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(AccionConstructiva entity)
        {
            _context.AccionesCons.Add(entity);
        }

        public void Delete(AccionConstructiva entity)
        {
            _context.AccionesCons.Remove(entity);
        }

        public IEnumerable<AccionConstructiva> GetAll()
        {
            return _context.AccionesCons;
        }

        public AccionConstructiva Update(AccionConstructiva entity, AccionConstructiva toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe la Especialidad que se quiere modificar");

            toUpd.Especialidad = entity.Especialidad ?? toUpd.Especialidad;
            toUpd.ManoObra = entity.ManoObra ?? toUpd.ManoObra;
            toUpd.Materiales = entity.Materiales ?? toUpd.Materiales;
            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.ObjetoObra = entity.ObjetoObra ?? toUpd.ObjetoObra;

            _context.AccionesCons.Update(toUpd);

            return toUpd;
        }

        public AccionConstructiva GetAccionCons(string nombre, ObjetoObra objO, string tipoPlan)
        {
            return _context.AccionesCons.Where(ac => ac.Nombre.ToLower() == nombre.ToLower() &&
                                                ac.ObjetoObra.ObjetoObraID == objO.ObjetoObraID &&
                                                ac.Plan.TipoPlan == tipoPlan)
                                                .SingleOrDefault();
        }

    }
}
