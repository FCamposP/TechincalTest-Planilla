using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Planilla.DTO.Componente;
using Planilla.DTO;
using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;

namespace Planilla.Services
{
    public class ComponenteService : BaseService<Componente>
    {
        private readonly IMapper _mapper;

        public ComponenteService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<ComponenteDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<ComponenteDTO>> response = new ResponseWrapperDTO<IList<ComponenteDTO>>();
            try
            {
                var componentes = await _dBContext.Componente.Include(x => x.Padre).Include(x => x.TipoComponente).OrderByDescending(x => x.Creado).ToListAsync();
                if (componentes != null)
                {
                    response.Data = _mapper.Map<List<ComponenteDTO>>(componentes);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ComponenteDTO>> GetByIdDTO(int id)
        {
            ResponseWrapperDTO<ComponenteDTO> response = new ResponseWrapperDTO<ComponenteDTO>();
            try
            {
                var c = await _dBContext.Componente.Where(x => x.ComponenteId == id).FirstOrDefaultAsync();
                if (c != null)
                {
                    response.Data = _mapper.Map<Componente, ComponenteDTO>(c);

                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ComponenteDTO>> ActualizarDTO(ComponenteDTO registro, int userId)
        {
            ResponseWrapperDTO<ComponenteDTO> response = new ResponseWrapperDTO<ComponenteDTO>();
            try
            {
                Componente registroGuardar = new Componente();
                registroGuardar = _mapper.Map<ComponenteDTO, Componente>(registro);
                registroGuardar.Padre = null;
                registroGuardar.TipoComponente = null;
                var result = await Actualizar(registroGuardar, userId);
                response.Data = _mapper.Map<Componente, ComponenteDTO>(result.Data ?? new Componente());


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró actualizar el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ComponenteDTO>> CrearDTO(ComponenteDTO registro, int userId)
        {
            ResponseWrapperDTO<ComponenteDTO> response = new ResponseWrapperDTO<ComponenteDTO>();
            try
            {
                Componente registroGuardar = new Componente();
                registroGuardar = _mapper.Map<ComponenteDTO, Componente>(registro);
                registroGuardar.Padre = null;
                registroGuardar.TipoComponente = null;
                var result = await Crear(registroGuardar, userId);
                response.Data = _mapper.Map<Componente, ComponenteDTO>(result.Data ?? new Componente());

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ComponenteDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<ComponenteDTO> response = new ResponseWrapperDTO<ComponenteDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<Componente, ComponenteDTO>(result.Data ?? new Componente());

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            ResponseWrapperDTO<int> response = new ResponseWrapperDTO<int>();
            try
            {
                foreach (int id in ids)
                {
                    var result = await Eliminar(id, userId);
                    if (result != null)
                    {
                        response.Data++;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron eliminar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        /// <summary>
        /// Obtiene la estructura de navegacion permitida para un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<List<ComponenteNavigation>>> ObtenerNavegacion(int userId)
        {
            ResponseWrapperDTO<List<ComponenteNavigation>> response = new ResponseWrapperDTO<List<ComponenteNavigation>>();
            try
            {
                
                List<Componente> componentesProcesar = new List<Componente>();
                List<ComponenteNavigation> opcionesSinAgrupar = new List<ComponenteNavigation>();
                var rolesUsuario = await _dBContext.RolUsuario.Include(x => x.Rol).Where(x => x.UsuarioId == userId).ToListAsync();
                if (rolesUsuario != null)
                {
                    //si es super usuario obtener todas las opciones
                    var superUsuario = rolesUsuario.Where(x => x.Rol.EsSuperUsuario == true).FirstOrDefault();
                    if (superUsuario != null)
                    {
                        componentesProcesar = await _dBContext.Componente.Where(x => x.TipoComponente.Codigo == "VISTA" || x.TipoComponente.Codigo == "GRUPO").ToListAsync();
                    }
                    else
                    {
                        foreach (var rolUser in rolesUsuario)
                        {
                            List<RolPermiso> rolPermisos= await _dBContext.RolPermiso.Include(x=>x.Componente).Where(x=>x.RolId==rolUser.RolId && x.Componente.TipoComponente.Codigo == "VISTA" || x.Componente.TipoComponente.Codigo == "GRUPO").ToListAsync();
                            List<Componente> componenteRol = new List<Componente>();
                            foreach (var role in rolPermisos)
                            {
                                componenteRol.Add(role.Componente);
                            }
                            //var componenteRol = await _dBContext.Componentes.Where(x => x.RolPermisos.Where(y => y.RolId == rolUser.RolId && y.Activo==true).ToList().Count > 1 && x.TipoComponente.Codigo == "VISTA" || x.TipoComponente.Codigo == "GRUPO").ToListAsync();
                            componentesProcesar.AddRange(componenteRol);
                        }
                        componentesProcesar = componentesProcesar.Distinct().ToList();
                    }
                    response.Data = new List<ComponenteNavigation>();

                    foreach (var componente in componentesProcesar)
                    {
                        ComponenteNavigation opcionNavegacion = new ComponenteNavigation();
                        opcionNavegacion.Id = componente.ComponenteId;
                        opcionNavegacion.Label = componente.NombreMostrar;
                        opcionNavegacion.Icon = componente.Icon;
                        opcionesSinAgrupar.Add(opcionNavegacion);
                    }
                    var componentesGrupos = componentesProcesar.Where(x => x.PadreId == null).ToList();
                    foreach (var grupo in componentesGrupos)
                    {
                        var componenteGrupo = opcionesSinAgrupar.Where(x => x.Id == grupo.ComponenteId).FirstOrDefault();
                        var componentesVistas = componentesProcesar.Where(x => x.PadreId == grupo.ComponenteId).ToList();
                        componenteGrupo.Items = new List<ComponenteNavigation>();
                        foreach (var vista in componentesVistas)
                        {
                            var opcionVista = opcionesSinAgrupar.Where(x => x.Id == vista.ComponenteId).FirstOrDefault();
                            opcionVista.RouterLink = new List<string>();
                            opcionVista.RouterLink.Add(vista.Url);
                            componenteGrupo.Items.Add(opcionVista);
                        }
                        response.Data.Add(componenteGrupo);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }
    }
}
