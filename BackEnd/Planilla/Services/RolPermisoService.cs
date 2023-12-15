using Microsoft.EntityFrameworkCore;
using Planilla.DTO;
using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;

namespace Planilla.Services
{
    public class RolPermisoService : BaseService<RolPermiso>, IAutorizacionRolPermiso
    {
        private readonly ApiDBContext _dBContext;

        public RolPermisoService(ApiDBContext context, IAppSettingsModule appSettingsModule) : base(context, appSettingsModule)
        {
            _dBContext = context;
        }

        /// <summary>
        /// Metodo encargado para valiar si un endpoint consultado esta permitodo para un usuario
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public bool PermisoEndpoint(string endpoint, int usuarioId)
        {
            bool autorizado = false;

            try
            {
                var rolesUsuario = _dBContext.RolUsuario.Include(x=>x.Rol).Where(x => x.UsuarioId == usuarioId).ToList();

                foreach (var rol in rolesUsuario)
                {
                    if(rol.Rol.EsSuperUsuario == true)
                    {
                        autorizado = true;
                        break;
                    }

                    var permiso = _dBContext.RolPermiso.Where(x => x.Componente.Url == endpoint && x.Rol.RolId == rol.RolId).FirstOrDefault();
                    if (permiso != null)
                    {
                        autorizado = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                autorizado =false;
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("RolPermisoService", "PermisoEndpoint");
                exceptionHandler.SaveException(excepcion);
            }
            return autorizado;
        }

        public async Task<ResponseWrapperDTO<IList<RolPermisoDTO>>> GetAll()
        {
            ResponseWrapperDTO<IList<RolPermisoDTO>> response = new ResponseWrapperDTO<IList<RolPermisoDTO>>();
            try
            {
                var rolPermisos = await _dBContext.RolPermiso.Where(x=> x.Activo == true).OrderByDescending(x => x.Creado).ToListAsync();
                if(rolPermisos != null)
                {
                    response.Data = rolPermisos.Select(x => new RolPermisoDTO()
                    {
                        RolPermisoId = x.RolPermisoId,
                        //Rol = x.Rol.Nombre,
                        //Componente = x.Componente.Nombre,
                        //ComponenteId = x.ComponenteId,
                        RolId = x.RolId
                    }).ToList();
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
