
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Entities;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class RolUserController : ControllerBaseCustom
    {
        readonly protected RolUsuarioService _rolUserService;

        public RolUserController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _rolUserService = new RolUsuarioService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<RolUsuarioDTO>>> Get()
        {
            return await _rolUserService.GetAll();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<RolUsuario>> GetById(int id)
        {
            return await _rolUserService.GetById(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<RolUsuario>> Actualizar(RolUsuario entity, int userId)
        {
            return await _rolUserService.Actualizar(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<RolUsuario>> Crear(RolUsuario entity, int userId)
        {
            //CASTEAR OBJETO
            // AUTOMAPPER
            return await _rolUserService.Crear(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<RolUsuario>> Eliminar(int id, int userId)
        {
            return await _rolUserService.Eliminar(id, userId);
        } 
        
        
        [HttpGet, Route("GetRolesPorUsuario")]
        public async Task<ResponseWrapperDTO<List<RolPermisoUsuarioDTO>>> GetRolesPorUsuario(int userId)
        {
            return await _rolUserService.GetRolesPorUsuario(userId);
        }
    }
}
