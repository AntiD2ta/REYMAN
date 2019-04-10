using System;
using System.Collections.Generic;
using System.Text;
using BizDbAccess.GenericInterfaces;
using BizData.Entities;
using DataLayer.EfCode;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class PlanActualDbAccess : IEntityDbAccess<PlanActual>
    {
        private readonly EfCoreContext _context;

        public PlanActualDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(PlanActual entity)
        {
            _context.PlanesActuales.Add(entity);
        }

        public void Delete(PlanActual entity)
        {
            _context.PlanesActuales.Remove(entity);
        }

        public IEnumerable<PlanActual> GetAll() => _context.PlanesActuales;

        public PlanActual Update(PlanActual entity, PlanActual toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("No existe el Plan que se quiere modificar");

            toUpd.Plan = entity.Plan ?? toUpd.Plan;

            _context.PlanesActuales.Update(toUpd);

            return toUpd;
        }

        public PlanActual GetPlanActual(string nombreUO, string tipoPlan, int año)
        {
            return _context.PlanesActuales.Where(pa => pa.UnidadOrganizativa.Nombre == nombreUO &&
                                                       pa.Plan.Año == año && pa.Plan.TipoPlan == tipoPlan)
                                                       .SingleOrDefault();
        }

        public List<PlanActual> GetPlanActual(string nombreUO)
        {
            return _context.PlanesActuales.Where(pa => pa.UnidadOrganizativa.Nombre == nombreUO).ToList();
        }

        public PlanActual AddOrUpdatePlan(string nombreUO, Plan plan)
        {
            var uos = _context.PlanesActuales.Where(pa => pa.UnidadOrganizativa.Nombre == nombreUO).ToList();
            bool founded = false;
            PlanActual toUpd = new PlanActual();

            if (uos == null)
                throw new InvalidOperationException($"No existe una unidad organizativa con nombre {nombreUO}");

            //Try to find a existing plan equals to plan to update that column in the database.
            for (int i = 0; i < uos.Count(); i++)
            {
                var uo = uos[i];

                if (uo.Plan.Año == plan.Año && uo.Plan.TipoPlan == plan.TipoPlan &&
                    uo.Plan.Estado == plan.Estado)
                {
                    uo.Plan = plan;
                    toUpd = uo;
                    founded = true;
                    break;
                }
            }

            //if a existing plan was not founded, add that plan
            if (!founded)
            {
                for (int i = 0; i < uos.Count; i++)
                {
                    var uo = uos[i];

                    if (uo.Plan == null)
                    {
                        uo.Plan = plan;
                        toUpd = uo;
                        break;
                    }
                }
            }

            _context.PlanesActuales.Update(toUpd);
            return toUpd;
        }
    }
}
