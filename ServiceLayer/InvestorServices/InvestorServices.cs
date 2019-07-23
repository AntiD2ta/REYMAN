﻿using BizData.Entities;
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
        private readonly RunnerWriteDb<UnidadMedidaCommand, UnidadMedida> _runnerUnidadMedida;
        private readonly RunnerWriteDb<EspecialidadCommand, Especialidad> _runnerEspecialidad;

        private readonly AccionConstructivaDbAccess _accionConstructivaDbAccess;
        private readonly AccionC_MaterialDbAccess _accionC_MaterialDbAccess;
        private readonly EspecialidadDbAccess _especialidadDbAccess;
        private readonly MaterialDbAccess _materialDbAccess;
        private readonly ManoObraDbAccess _manoObraDbAccess;
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
            _runnerUnidadMedida = new RunnerWriteDb<UnidadMedidaCommand, UnidadMedida>(
                new RegisterUnidadMedidaAction(new UnidadMedidaDbAccess(_context)), _context);
            _runnerEspecialidad = new RunnerWriteDb<EspecialidadCommand, Especialidad>(
                new RegisterEspecialidadAction(new EspecialidadDbAccess(_context)), _context);

            _planDbAccess = new PlanDbAccess(_context);
            _inmuebleDbAccess = new InmuebleDbAccess(_context);
            _objetoObraDbAccess = new ObjetoObraDbAccess(_context);
            _unidadOrganizativaDbAccess = new UnidadOrganizativaDbAccess(_context);
            _accionConstructivaDbAccess = new AccionConstructivaDbAccess(_context);
            _especialidadDbAccess = new EspecialidadDbAccess(_context);
            _materialDbAccess = new MaterialDbAccess(_context);
            _unidadMedidaDbAccess = new UnidadMedidaDbAccess(_context);
            _accionC_MaterialDbAccess = new AccionC_MaterialDbAccess(_context);
            _manoObraDbAccess = new ManoObraDbAccess(_context);
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

        public Plan GetPlan(int año, string tipo, UnidadOrganizativa unidadOrganizativa)
        {
            return _planDbAccess.GetPlan(año, tipo, unidadOrganizativa);
        }

        public Plan UpdatePlan(Plan entity, Plan toUpd)
        {
            var plan = _planDbAccess.Update(entity, toUpd);
            _context.Commit();
            return plan;
        }

        public void DeletePlan(Plan plan)
        {
            for (int i = 0; i < plan.AccionesConstructivas.Count(); i++)
            {
                DeleteAC(plan.AccionesConstructivas.First());
            }
            _planDbAccess.Delete(plan);
            _context.Commit();
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
                _inmuebleDbAccess.GetInmueble(entity.UnidadOrganizativa, entity.Direccion) != null)
            {
                throw new InvalidOperationException($"Ya existe un Inmueble con direccion {entity.Direccion}");
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
                entity.Inmueble.UnidadOrganizativa.Nombre) != null)
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

        public ObjetoObra GetObjetoObra(string nombre, string dirInmueble, string nombreUO) => _objetoObraDbAccess.GetObjObra(nombre, dirInmueble, nombreUO);

        public long RegisterAccionCons(AccionConsCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            cmd.Materiales = cmd.ListItems.Where(i => i.nameMaterial != null && (i.precioCUC != null || i.precioCUP != null))
                                          .Select(i => (i.nameMaterial, i.unidadMedida, i.precioCUP, i.precioCUC));

            cmd.Plan = _planDbAccess.GetPlan(cmd.PlanID);
            cmd.Especialidad = _especialidadDbAccess.GetEspecialidad(cmd.EspecialidadID);
            cmd.ObjetoObra = _objetoObraDbAccess.GetObjObra(cmd.ObjetoObraID);

            var data = cmd.ToAC_M(_unidadMedidaDbAccess.GetAll().ToList(), _materialDbAccess.GetAll().ToList());
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

            if (ac.Materiales != null)
            {
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
            }

            errors = null;
            return ac.AccionConstructivaID;
        }

        public AccionConstructiva UpdateAccionConstructiva(AccionConstructiva entity, AccionConstructiva toUpd)
        {
            if (entity.Nombre != null && entity.Nombre != toUpd.Nombre &&
                _accionConstructivaDbAccess.GetAccionCons(entity.Nombre,
                entity.ObjetoObra, entity.Plan) != null)
            {
                throw new Exception($"Ya existe una Accion Constructiva con nombre {entity.Nombre} en" +
                    $" {entity.ObjetoObra.Nombre}");
            }

            var ac = _accionConstructivaDbAccess.Update(entity, toUpd);
            _context.Commit();
            return ac;
        }

        public int DeleteAccionC_Material(int id)
        {
            var mat = _accionC_MaterialDbAccess.GetAccionC_Material(id);
            int ac_id = mat.AccionConstructiva.AccionConstructivaID;
            _accionC_MaterialDbAccess.Delete(mat);
            _context.Commit();
            return ac_id;
        }

        public void DeleteAC(AccionConstructiva ac)
        {
            _accionConstructivaDbAccess.Delete(ac);
            _context.Commit();
        }

        public AccionConstructiva GetAC(int id)
        {
            return _accionConstructivaDbAccess.GetAccionCons(id);
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

        public int RegisterMaterial(MaterialCommand cmd, out IImmutableList<ValidationResult> errors)
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
            if (entity.Nombre != null && _materialDbAccess.GetMaterial(entity.Nombre, entity.UnidadMedida.Nombre) == null)
            {
                var mat = _materialDbAccess.Update(entity, toUpd.Material);
                _context.Commit();
            }

            //if (entity.UnidadMedida != null)
            //{
            //    MaterialCommand cmd = new MaterialCommand(toUpd, entity);
            //    var id = RegisterMaterial(cmd, out errors);
            //    if (id != -1)
            //    {
            //        return _materialDbAccess.GetMaterial(id);
            //    }
            //    return null;
            //}

            return toUpd.Material;
        }

        public void DeleteMaterial(Material entity)
        {
            _materialDbAccess.Delete(entity);
            _context.Commit();
        }

        public void RemoveMaterialFromAC(int id)
        {
            var item = _accionC_MaterialDbAccess.GetAccionC_Material(id);
            _accionC_MaterialDbAccess.Delete(item);
            _context.Commit();
        }

        public void AddAcMaterial(AccionConstructiva ac , AccionC_Material acm)
        {
            ac.Materiales.Add(acm);
            _context.Commit();
        }

        public int RegisterUnidadMedida(UnidadMedidaCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            var unidadMedida = _runnerUnidadMedida.RunAction(cmd);

            if (_runnerUnidadMedida.HasErrors)
            {
                errors = _runnerUnidadMedida.Errors;
                return -1;
            }

            errors = null;
            return unidadMedida.UnidadMedidaID;
        }

        public void DeleteUnidadMedida(UnidadMedida entity)
        {
            _unidadMedidaDbAccess.Delete(entity);
            _context.Commit();
        }

        public UnidadMedida GetUM(string nombre) => _unidadMedidaDbAccess.GetUM(nombre);

        public int RegisterEspecialidad(EspecialidadCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            var especialidad = _runnerEspecialidad.RunAction(cmd);

            if (_runnerEspecialidad.HasErrors)
            {
                errors = _runnerEspecialidad.Errors;
                return -1;
            }

            errors = null;
            return especialidad.EspecialidadID;
        }

        public void DeleteEspecialidad(Especialidad entity)
        {
            _especialidadDbAccess.Delete(entity);
            _context.Commit();
        }

        public Especialidad GetEspecialidad(string tipo) => _especialidadDbAccess.GetEspecialidad(tipo);

        public AccionC_Material UpdateACM(AccionC_Material entity, AccionC_Material toUpd)
        {
            //foreach (var item in toUpd.AccionConstructiva.Materiales)
            //{
            //    if (entity.Equals(item))
            //        throw new InvalidOperationException("Ya existe ese material en la acción constructiva actual");
            //}

            toUpd = _accionC_MaterialDbAccess.Update(entity, toUpd);
            _context.Commit();

            return toUpd;
        }

        public ManoObra UpdateManoObra(ManoObra entity, ManoObra toUpd)
        {
            toUpd = _manoObraDbAccess.Update(entity, toUpd);
            _context.Commit();

            return toUpd;
        }
    }
}