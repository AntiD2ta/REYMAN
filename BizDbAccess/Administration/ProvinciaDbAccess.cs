﻿using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Authentication
{
    public class ProvinciaDbAccess : IEntityDbAccess<Provincia>
    {
        private readonly EfCoreContext _context;

        public ProvinciaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Provincia entity)
        {
            _context.Provincias.Add(entity);
            _context.Commit();
        }

        public void Delete(Provincia entity)
        {
            if (_context.Provincias.Find(entity.ProvinciaID) != null)
            {
                _context.Provincias.Remove(entity);
                _context.Commit();
            }
        }

        public IEnumerable<Provincia> GetAll()
        {
            return _context.Provincias;
        }

        public void Update(Provincia entity)
        {
            if (_context.Provincias.Find(entity.ProvinciaID) != null)
            {
                _context.Provincias.Update(entity);
                _context.Commit();
            }
        }
    }
}
