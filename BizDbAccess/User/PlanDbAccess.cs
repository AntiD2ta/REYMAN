using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.User
{
    public class PlanDbAccess : IEntityDbAccess<Plan>
    {
        public readonly EfCoreContext _context;

        public PlanDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Plan entity)
        {
            _context.Planes.Add(entity);
        }

        public void Delete(Plan entity)
        {
            if (_context.Planes.Find(entity.PlanID) != null)
            {
                _context.Planes.Remove(entity);
                _context.Commit();
            }
        }

        public IEnumerable<Plan> GetAll()
        {
            return _context.Planes;
        }

        public void Update(Plan entity)
        {
            if (_context.Planes.Find(entity.PlanID) != null)
            {
                _context.Planes.Update(entity);
                _context.Commit();
            }
        }
    }
}
