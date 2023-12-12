using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class RolController : ControllerBaseCustom
    {
        readonly protected RolService _rolService;
        public RolController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _rolService = new RolService(context, appSettingsModule,mapper);
        }
        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<RolDTO>>> Get()
        {
            return await _rolService.GetAllDTO();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<RolDTO>> GetById(int id)
        {
            return await _rolService.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<RolDTO>> Actualizar(RolDTO entity, int userId = -1)
        {
            return await _rolService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<RolDTO>> Crear(RolDTO entity, int userId)
        {
            return await _rolService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<RolDTO>> Eliminar(int id, int userId)
        {
            return await _rolService.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _rolService.EliminarMultiples(ids, userId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<List<RolTree>>> GetRolPermisoTree()
        {
            return await _rolService.GetComponentesTree();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<List<RolDTO>>> GetRolesPorUsuario(int userId)
        {
            return await _rolService.GetRolesPorUsuario(userId);
        }
    }
}
