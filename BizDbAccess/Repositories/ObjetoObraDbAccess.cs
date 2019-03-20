using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.User
{
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
                _context.Commit();
            }
        }

        public IEnumerable<ObjetoObra> GetAll()
        {
            return _context.ObjetosObra;
        }

        public void Update(ObjetoObra entity)
        {
            if (_context.ObjetosObra.Find(entity.ObjetoObraID) != null)
            {
                _context.ObjetosObra.Update(entity);
                _context.Commit();
            }
        }
    }
}
