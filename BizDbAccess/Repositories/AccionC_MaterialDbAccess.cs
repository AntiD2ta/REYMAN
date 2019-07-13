using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class AccionC_MaterialDbAccess : IEntityDbAccess<AccionC_Material>
    {
        private readonly EfCoreContext _context;

        public AccionC_MaterialDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(AccionC_Material entity)
        {
            _context.AccCons_Mat.Add(entity);
        }

        public void Delete(AccionC_Material entity)
        {
            _context.AccCons_Mat.Remove(entity);
        }

        public IEnumerable<AccionC_Material> GetAll() => _context.AccCons_Mat;

        public AccionC_Material Update(AccionC_Material entity, AccionC_Material toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe la AccionC_Material que se quiere modificar");

            toUpd.Cantidad = entity.Cantidad ?? toUpd.Cantidad;
            toUpd.Material = entity.Material ?? toUpd.Material;
            toUpd.PrecioCUC = entity.PrecioCUC ?? entity.PrecioCUC;
            toUpd.PrecioCUP = entity.PrecioCUP ?? entity.PrecioCUP;

            _context.AccCons_Mat.Update(toUpd);

            return toUpd;
        }

        public AccionC_Material GetAccionC_Material(int id) => _context.AccCons_Mat.Find(id);
    }
}
