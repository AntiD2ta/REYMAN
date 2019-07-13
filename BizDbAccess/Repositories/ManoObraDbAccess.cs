using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class ManoObraDbAccess : IEntityDbAccess<ManoObra>
    {
        private readonly EfCoreContext _context;

        public ManoObraDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(ManoObra entity)
        {
            _context.ManosObra.Add(entity);
        }

        public void Delete(ManoObra entity)
        {
            _context.ManosObra.Remove(entity);
        }

        public IEnumerable<ManoObra> GetAll() => _context.ManosObra;

        public ManoObra Update(ManoObra entity, ManoObra toUpd)
        {
            if (toUpd == null)
                throw new Exception("No existe la Mano de Obra que se quiere modificar");

            toUpd.Cantidad = entity.Cantidad == 0 ? toUpd.Cantidad : entity.Cantidad;
            toUpd.PrecioCUC = entity.PrecioCUC ?? toUpd.PrecioCUC;
            toUpd.PrecioCUP = entity.PrecioCUP ?? toUpd.PrecioCUP;
            toUpd.UnidadMedida = entity.UnidadMedida ?? toUpd.UnidadMedida;

            _context.ManosObra.Update(toUpd);

            return toUpd;
        }
    }
}
