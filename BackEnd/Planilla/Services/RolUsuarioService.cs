using Microsoft.Extensions.Options;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Planilla.DataAccess;
using Planilla.Abstractions;

namespace Planilla.Services
{
    public class RolUsuarioService : BaseService<RolUsuario>
    {

        private readonly IMapper _mapper;

        public RolUsuarioService(ApiDBContext context, IAppSettingsModule appSettingsModule) : base(context, appSettingsModule)
        {

        }

        public RolUsuarioService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }



        public async Task<ResponseWrapperDTO<List<RolPermisoUsuarioDTO>>> GetRolesPorUsuario(int usuarioId)
        {
            ResponseWrapperDTO<List<RolPermisoUsuarioDTO>> response = new ResponseWrapperDTO<List<RolPermisoUsuarioDTO>>();
            try
            {

                var registros = await _dBContext.RolUsuario.Include(x => x.Rol).Where(x => x.UsuarioId == usuarioId).ToListAsync();
                var infoRoles = registros.Select(x => x.Rol).ToList();
                var permisos = _dBContext.RolPermiso.Include(x => x.Componente).Where(x => infoRoles.Contains(x.Rol)).ToList();
                bool esSuperUsuario = infoRoles.Where(x => x.EsSuperUsuario == true).Any() ? true : false;
                if (esSuperUsuario)
                {
                    List<RolPermisoUsuarioDTO> listado = new List<RolPermisoUsuarioDTO>();
                    listado.Add(new RolPermisoUsuarioDTO()
                    {
                        superUsuario = true
                    });
                    response.Data = listado;
                    return response;
                }

                response.Data = permisos.Select(c => new RolPermisoUsuarioDTO()
                {
                    ComponenteId = c.ComponenteId,
                    NombreComponente = c.Componente?.Nombre,
                    Padre = c.Componente?.Padre?.Url,
                    RolId = c.RolId,
                    RolPermisoId = c.RolPermisoId,
                    Url = c.Componente?.Url,
                    superUsuario = false
                }).ToList();


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<IList<RolUsuarioDTO>>> GetAll()
        {
            ResponseWrapperDTO<IList<RolUsuarioDTO>> response = new ResponseWrapperDTO<IList<RolUsuarioDTO>>();
            return response;
        }
    }
}
