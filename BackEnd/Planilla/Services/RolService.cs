using Planilla.Entities;
using Microsoft.EntityFrameworkCore;
using Planilla.DTO.Others;
using AutoMapper;
using System.Data;
using Planilla.DTO.Componente;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Planilla.DataAccess;
using Planilla.Abstractions;
using Planilla.DTO;

namespace Planilla.Services
{
    public class RolService : BaseService<Rol>
    {
        private readonly IMapper _mapper;
        private readonly ComponenteService _componenteService;
        private readonly RolUsuarioService _rolUsuarioService;
        private readonly RolPermisoService _rolPermisoService;

        public RolService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
            _componenteService = new ComponenteService(context, appSettingsModule, mapper);
            _rolUsuarioService = new RolUsuarioService(context, appSettingsModule);
            _rolPermisoService = new RolPermisoService(context, appSettingsModule);
        }
        public async Task<ResponseWrapperDTO<IList<RolDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<RolDTO>> response = new ResponseWrapperDTO<IList<RolDTO>>();
            try
            {
                var registros = await _dBContext.Rol.OrderByDescending(x => x.Creado).ToListAsync();
                response.Data = _mapper.Map<List<Rol>, List<RolDTO>>(registros);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<RolDTO>> GetByIdDTO(int id)
        {
            ResponseWrapperDTO<RolDTO> response = new ResponseWrapperDTO<RolDTO>();
            try
            {
                var registro = await _dBContext.Rol.Where(x => x.RolId == id).FirstOrDefaultAsync();
                if (registro != null)
                {
                    var dataRol = _mapper.Map<Rol, RolDTO>(registro);
                    if (dataRol != null)
                    {
                        response.Data = dataRol;
                        var rolUsers = await _dBContext.RolUsuario.Where(x => x.RolId == id).ToListAsync();
                        var rolPermisos = await _dBContext.RolPermiso.Where(x => x.RolId == id).ToListAsync();
                        response.Data.RolUsuarios = _mapper.Map<List<RolUsuario>, List<RolUsuarioDTO>>(rolUsers);
                        response.Data.RolPermisos = _mapper.Map<List<RolPermiso>, List<RolPermisoDTO>>(rolPermisos);
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

        public async Task<ResponseWrapperDTO<RolDTO>> ActualizarDTO(RolDTO registro, int userId)
        {
            ResponseWrapperDTO<RolDTO> response = new ResponseWrapperDTO<RolDTO>();
            try
            {
                Rol registroGuardar = new Rol();
                registroGuardar = _mapper.Map<RolDTO, Rol>(registro);
                var result = await Actualizar(registroGuardar, userId);

                registroGuardar = result.Data;
                //crear o desactivar nuevos registros de rolUsuarios
                var rolUsuarios = await _dBContext.RolUsuario.Where(x => x.RolId == registro.RolId).ToListAsync();
                //agregar nuevos
                foreach (var item in registro.RolUsuarios)
                {
                    var usuarioGuardado = rolUsuarios.Where(x => x.UsuarioId == item.UsuarioId).FirstOrDefault();
                    if (usuarioGuardado == null)
                    {
                        var nuevoRolUsuario = new RolUsuario()
                        {
                            UsuarioId = item.UsuarioId,
                            RolId = registroGuardar.RolId
                        };
                        await _rolUsuarioService.Crear(nuevoRolUsuario, userId);
                    }
                }
                //desactivar rolUsuarios quitados
                foreach (RolUsuario rolUser in rolUsuarios)
                {
                    var usuarioAgregado = registro.RolUsuarios.Where(x => x.UsuarioId == rolUser.UsuarioId).FirstOrDefault();
                    if (usuarioAgregado == null)
                    {
                        await _rolUsuarioService.Eliminar(rolUser.RolUsuarioId, userId);
                    }
                }

                //crear o desactivar nuevos registros de rolPermiso
                var rolPermisos = await _dBContext.RolPermiso.Where(x => x.RolId == registro.RolId).ToListAsync();
                //agregar nuevos
                foreach (var item in registro.RolPermisos)
                {
                    var usuarioGuardado = rolPermisos.Where(x => x.ComponenteId == item.ComponenteId).FirstOrDefault();
                    if (usuarioGuardado == null)
                    {
                        var nuevoRolPermiso = new RolPermiso()
                        {
                            ComponenteId = item.ComponenteId,
                            RolId = registroGuardar.RolId
                        };
                        await _rolPermisoService.Crear(nuevoRolPermiso, userId);
                    }
                }
                //desactivar rolUsuarios quitados
                foreach (RolPermiso rolPerm in rolPermisos)
                {
                    var usuarioAgregado = registro.RolPermisos.Where(x => x.ComponenteId == rolPerm.ComponenteId).FirstOrDefault();
                    if (usuarioAgregado == null)
                    {
                        await _rolPermisoService.Eliminar(rolPerm.RolPermisoId, userId);
                    }
                }

                response.Data = _mapper.Map<Rol, RolDTO>(registroGuardar);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron actualizar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<RolDTO>> CrearDTO(RolDTO registro, int userId)
        {
            ResponseWrapperDTO<RolDTO> response = new ResponseWrapperDTO<RolDTO>();
            try
            {
                Rol registroGuardar = new Rol();
                registroGuardar = _mapper.Map<RolDTO, Rol>(registro);
                var result = await Crear(registroGuardar, userId);
                if (result.Data != null)
                {
                    registroGuardar = result.Data;
                    //agregar nuevos usuarios
                    foreach (var item in registro.RolUsuarios)
                    {
                        var nuevoRolUsuario = new RolUsuario()
                        {
                            UsuarioId = item.UsuarioId,
                            RolId = registroGuardar.RolId
                        };
                        await _rolUsuarioService.Crear(nuevoRolUsuario, userId);
                    }

                    //agregar nuevos roles
                    foreach (var item in registro.RolPermisos)
                    {
                        var nuevoRolPermiso = new RolPermiso()
                        {
                            ComponenteId = item.ComponenteId,
                            RolId = registroGuardar.RolId
                        };
                        await _rolPermisoService.Crear(nuevoRolPermiso, userId);
                    }
                    response.Data = _mapper.Map<Rol, RolDTO>(registroGuardar);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<RolDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<RolDTO> response = new ResponseWrapperDTO<RolDTO>();
            try
            {
                var result= EliminarDependientes(id, userId);
                response.Data = _mapper.Map<Rol, RolDTO>(result.Result ?? new Rol());

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
                    var rolUsuarios = await _dBContext.RolUsuario.Where(x => x.RolId == id).ToListAsync();
                    
                    if ((await EliminarDependientes(id,userId))!=null)
                    {
                        response.Data++;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Data = 1;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron eliminar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        private async Task<Rol> EliminarDependientes(int rolId, int userId)
        {
            Rol result=null;
            //Eliminacion de rolesusuarios
            var rolUsuarios = await _dBContext.RolUsuario.Where(x => x.RolId == rolId).ToListAsync();
            foreach (var item in rolUsuarios)
            {
                var desactivarRolUsuario = await _rolUsuarioService.Eliminar(item.RolUsuarioId, userId);
            }

            //eliminacion de permisos
            var rolPermisos = await _dBContext.RolPermiso.Where(x => x.RolId == rolId).ToListAsync();
            foreach (var item in rolPermisos)
            {
                var desactivarRolUsuario = await _rolPermisoService.Eliminar(item.RolPermisoId, userId);
            }

            //eliminacion del rol
            var resultRol = await Eliminar(rolId, userId);
            if (resultRol.Data != null)
                result = resultRol.Data;

            return result;
        }

        public async Task<ResponseWrapperDTO<List<RolDTO>>> GetRolesPorUsuario(int usuarioId)
        {
            ResponseWrapperDTO<List<RolDTO>> response = new ResponseWrapperDTO<List<RolDTO>>();
            try
            {
                var registros = await _dBContext.RolUsuario.Include(x => x.Rol).Where(x => x.UsuarioId == usuarioId).ToListAsync();
                var infoRoles = registros.Select(x => x.Rol).ToList();
                response.Data = _mapper.Map<List<Rol>, List<RolDTO>>(infoRoles);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<List<RolTree>>> GetComponentesTree(bool esFrontOffice)
        {
            ResponseWrapperDTO<List<RolTree>> response = new ResponseWrapperDTO<List<RolTree>>() { Data = new List<RolTree>() };
            try
            {
                List<RolTree> nodos = new List<RolTree>();
                var responseComponentes = await _componenteService.GetAllDTO(esFrontOffice);
                if (responseComponentes.Data != null)
                {
                    foreach (var item in responseComponentes.Data)
                    {
                        RolTree nodo = new RolTree()
                        {
                            Key = item.ComponenteId.ToString(),
                            label = item.NombreMostrar,
                            data = item.Nombre,
                            icon = ""
                        };
                        nodos.Add(nodo);
                    }
                    var nodosSinPadres = responseComponentes.Data.Where(x => x.PadreId == null).ToList();
                    foreach (var item in nodosSinPadres)
                    {
                        var nodo = nodos.Where(x => x.Key == item.ComponenteId.ToString()).FirstOrDefault();
                        if (nodo != null)
                        {
                            nodo = AnidarNodos(nodos, responseComponentes.Data, nodo);
                            response.Data.Add(nodo);
                        }
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

        private RolTree AnidarNodos(List<RolTree> nodos, IList<ComponenteDTO> componentes, RolTree nodo)
        {
            var hijosNodo = componentes.Where(x => x.PadreId.ToString() == nodo.Key).ToList();

            foreach (var item in hijosNodo)
            {
                var nodoHijo = nodos.Where(x => x.Key == item.ComponenteId.ToString()).FirstOrDefault();
                if (nodoHijo != null)
                {
                    nodoHijo = AnidarNodos(nodos, componentes, nodoHijo);
                    nodo.Children.Add(nodoHijo);
                }
            }
            return nodo;
        }
    }
}
