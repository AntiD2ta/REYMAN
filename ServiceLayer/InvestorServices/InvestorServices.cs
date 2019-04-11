using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.User;
using BizLogic.Planning;
using BizLogic.Planning.Concrete;
using ServiceLayer.BizRunners;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BizDbAccess.Repositories;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.InvestorServices
{
    public class InvestorServices
    {
        private readonly RunnerWriteDb<PlanCommand, Plan> _runnerPlan;
        private readonly RunnerWriteDb<InmuebleCommand, Inmueble> _runnerInmueble;
        private readonly RunnerWriteDb<ObjObraCommand, ObjetoObra> _runnerObjObra;
        private readonly RunnerWriteDb<AccionConsCommand, AccionConstructiva> _runnerAccionCons;
        private readonly RunnerWriteDb<MaterialCommand, Material> _runnerMaterial;

        private readonly AccionConstructivaDbAccess _accionConstructivaDbAccess;
        private readonly EspecialidadDbAccess _especialidadDbAccess;
        private readonly MaterialDbAccess _materialDbAccess;
        private readonly PlanDbAccess _planDbAccess;
        private readonly InmuebleDbAccess _inmuebleDbAccess;
        private readonly ObjetoObraDbAccess _objetoObraDbAccess;
        private readonly UnidadOrganizativaDbAccess _unidadOrganizativaDbAccess;
        private readonly UnidadMedidaDbAccess _unidadMedidaDbAccess;
        private readonly IUnitOfWork _context;

        public InvestorServices(IUnitOfWork context)
        {
            _context = context;
            _runnerPlan = new RunnerWriteDb<PlanCommand, Plan>(
                new RegisterPlanAction(new PlanDbAccess(_context)), _context);
            _runnerInmueble = new RunnerWriteDb<InmuebleCommand, Inmueble>(
                new RegisterInmuebleAction(new InmuebleDbAccess(_context)), _context);
            _runnerAccionCons = new RunnerWriteDb<AccionConsCommand, AccionConstructiva>(
                new RegisterAccionConsAction(new AccionConstructivaDbAccess(_context)), _context);
            _runnerObjObra = new RunnerWriteDb<ObjObraCommand, ObjetoObra>(
                new RegisterObjObraAction(new ObjetoObraDbAccess(_context)), _context);
            _runnerMaterial = new RunnerWriteDb<MaterialCommand, Material>(
                new RegisterMaterialAction(new MaterialDbAccess(_context)), _context);

            _planDbAccess = new PlanDbAccess(_context);
            _inmuebleDbAccess = new InmuebleDbAccess(_context);
            _objetoObraDbAccess = new ObjetoObraDbAccess(_context);
            _unidadOrganizativaDbAccess = new UnidadOrganizativaDbAccess(_context);
            _accionConstructivaDbAccess = new AccionConstructivaDbAccess(_context);
            _especialidadDbAccess = new EspecialidadDbAccess(_context);
            _materialDbAccess = new MaterialDbAccess(_context);
            _unidadMedidaDbAccess = new UnidadMedidaDbAccess(_context);
        }

        public long RegisterPlan(PlanCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            var plan = _runnerPlan.RunAction(cmd);
            
            if (_runnerPlan.HasErrors)
            {
                errors = _runnerPlan.Errors;
                return -1;
            }

            errors = null;
            return plan.PlanID;
        }

        public Plan GetPlan(int año, string tipo)
        {
            return _planDbAccess.GetPlan(año, tipo);
        }

        public Plan UpdatePlan(Plan entity, Plan toUpd)
        {
            var plan = _planDbAccess.Update(entity, toUpd);
            _context.Commit();
            return plan;
        }

        public long RegisterInmueble(InmuebleCommand cmd, string nombreUO, out IImmutableList<ValidationResult> errors)
        {
            var uo = _unidadOrganizativaDbAccess.GetUO(nombreUO);
            cmd.UO = uo;

            var inm = _runnerInmueble.RunAction(cmd);

            if (_runnerInmueble.HasErrors)
            {
                errors = _runnerInmueble.Errors;
                return -1;
            }

            errors = null;
            return inm.InmuebleID;
        }

        public Inmueble UpdateInmueble(Inmueble entity, Inmueble toUpd)
        {
            if (entity.Direccion != null && entity.Direccion != toUpd.Direccion &&
                _inmuebleDbAccess.GetInmueble(entity.UO, entity.Direccion) != null)
            {
                throw new Exception($"Ya existe un Inmueble con direccion {entity.Direccion}");
            }

            var inmueble = _inmuebleDbAccess.Update(entity, toUpd);
            _context.Commit();
            return inmueble;
        }

        public void DeleteInmueble(Inmueble entity)
        {
            _inmuebleDbAccess.Delete(entity);
            _context.Commit();
        }

        public bool CheckForInmuebles(string nombreUO)
        {
            var uo = _unidadOrganizativaDbAccess.GetUO(nombreUO);
            return uo.Inmuebles.Any();
        }

        public Inmueble AddObjObraToInm(Inmueble entity, IEnumerable<ObjetoObra> objsObra)
        {
            if (entity.ObjetosDeObra == null)
            {
                entity.ObjetosDeObra = new List<ObjetoObra>();
            }

            _inmuebleDbAccess.AddObjObra(ref entity, objsObra);
            _context.Commit();
            return entity;
        }

        public long RegisterObjObra(ObjObraCommand vm, string dirInmueble, out IImmutableList<ValidationResult> errors)
        {
            vm.Inmueble = _inmuebleDbAccess.GetInmueble(_unidadOrganizativaDbAccess.GetUO(vm.nombreUO),
                            dirInmueble);

            var objObra = _runnerObjObra.RunAction(vm);

            if (_runnerObjObra.HasErrors)
            {
                errors = _runnerObjObra.Errors;
                return -1;
            }

            errors = null;
            return objObra.ObjetoObraID;
        }

        public ObjetoObra UpdateObjetoObra(ObjetoObra entity, ObjetoObra toUpd)
        {
            if (entity.Nombre != null && entity.Nombre != toUpd.Nombre &&
                _objetoObraDbAccess.GetObjObra(entity.Nombre, entity.Inmueble.Direccion,
                entity.Inmueble.UO.Nombre) != null)
            {
                throw new Exception($"Ya existe un Objeto de Obra con nombre {entity.Nombre}");
            }

            var objObra = _objetoObraDbAccess.Update(entity, toUpd);
            _context.Commit();
            return objObra;
        }

        public void DeleteObjetoObra(ObjetoObra entity)
        {
            _objetoObraDbAccess.Delete(entity);
            _context.Commit();
        }

        public long RegisterAccionCons(AccionConsCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            cmd.Plan = _planDbAccess.GetPlan(cmd.PlanID);
            cmd.Especialidad = _especialidadDbAccess.GetEspecialidad(cmd.TipoEspecialidad);
            cmd.ObjetoObra = _objetoObraDbAccess.GetObjObra(cmd.ObjetoObraID);

            var data = cmd.ToAC_M();
            cmd.MaterialPrecio = new List<(Material, decimal?, decimal?)>();
            foreach (var t in data)
            {
                if (TryGetMaterial(t.material, out var m))
                {
                    cmd.MaterialPrecio.Add((m, t.precioCUP, t.precioCUC));
                }
                else
                {
                    cmd.MaterialPrecio.Add((t.material, t.precioCUP, t.precioCUC));
                }
            }

            var mo = cmd.ToManoObra();
            var umMO = _unidadMedidaDbAccess.GetUM(mo.UnidadMedida.Nombre);
            if (umMO != null)
                mo.UnidadMedida = umMO;
            cmd.ManoObra = mo;

            var ac = _runnerAccionCons.RunAction(cmd);

            if (_runnerAccionCons.HasErrors)
            {
                errors = _runnerAccionCons.Errors;
                return -1;
            }

            var aux = ac.Materiales.ToArray();
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].Material.AccionesConstructivas != null)
                {
                    //finded remains false if exist the actual material has no ac as
                    //AccionConstructiva
                    bool finded = false;
                    AccionC_Material temp = new AccionC_Material();
                    foreach (var item in aux[i].Material.AccionesConstructivas)
                    {
                        temp = item;
                        if (item.AccionConstructiva.Nombre == ac.Nombre)
                        {
                            finded = true;
                            break;
                        }
                    }

                    if (!finded)
                    {
                        temp.AccionConstructiva = ac;
                    }
                }
                else
                {   //enter here if actual material not have AccionesContructivas initialized
                    //and initialize it.
                    aux[i].Material.AccionesConstructivas = new List<AccionC_Material>()
                    {
                        new AccionC_Material()
                        {
                            AccionConstructiva = ac,
                            Material = aux[i].Material
                        }
                    };
                    break;
                }
            }

            errors = null;
            return ac.AccionConstructivaID;
        }

        public AccionConstructiva UpdateAccionConstructiva(AccionConstructiva entity, AccionConstructiva toUpd)
        {
            if (entity.Nombre != null && entity.Nombre != toUpd.Nombre &&
                _accionConstructivaDbAccess.GetAccionCons(entity.Nombre,
                entity.ObjetoObra) != null)
            {
                throw new Exception($"Ya existe una Accion Constructiva con nombre {entity.Nombre} en" +
                    $" {entity.ObjetoObra.Nombre}");
            }

            var ac = _accionConstructivaDbAccess.Update(entity, toUpd);
            _context.Commit();
            return ac;
        }

        public bool TryGetMaterial(Material temp, out Material current)
        {
            if (temp.UnidadMedida.Nombre == null)
            {
                current = _materialDbAccess.GetMaterial(temp.Nombre);
            }
            else
            {
                current = _materialDbAccess.GetMaterial(temp.Nombre, temp.UnidadMedida.Nombre);
            }

            if (current == null)
            {
                return false;
            }
            return true;
        }

        public long RegisterMaterial(MaterialCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            var material = _runnerMaterial.RunAction(cmd);

            if (_runnerMaterial.HasErrors)
            {
                errors = _runnerMaterial.Errors;
                return -1;
            }

            errors = null;
            return material.MaterialID;
        }

        public Material UpdateMaterial(Material entity, AccionC_Material toUpd, out IImmutableList<ValidationResult> errors)
        {
            errors = null;
            if (entity.Nombre != null && entity.Nombre != toUpd.Material.Nombre &&
                _materialDbAccess.GetMaterial(entity.Nombre) != null)
            {
                throw new Exception($"Ya existe un Material con nombre {entity.Nombre}.");
            }

            if (entity.UnidadMedida != null)
            {
                MaterialCommand cmd = new MaterialCommand(toUpd, entity);
                var id = RegisterMaterial(cmd, out errors);
                if (id != -1)
                {
                    return _materialDbAccess.GetMaterial(id);
                }
                return null;
            }

            var mat = _materialDbAccess.Update(entity, toUpd.Material);
            _context.Commit();
            return mat;
        }

        public void RemoveMaterialFromAC(Material mat, AccionConstructiva ac)
        {
            foreach (var item in ac.Materiales)
            {
                if (item.Material.Equals(mat))
                {
                    //TODO: This needs to be tested
                    ac.Materiales.Remove(item);
                    _context.Commit();
                    break;
                }
            }
        }
    }
}