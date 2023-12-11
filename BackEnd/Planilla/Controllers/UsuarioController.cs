
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
    public class UsuarioController : ControllerBaseCustom
    {
        readonly protected UsuarioService _service;
        public UsuarioController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new UsuarioService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<UsuarioDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<UsuarioDTO>> GetById(int id)
        {
            return await _service.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<UsuarioDTO>> Actualizar(UsuarioDTO entity, int userId)
        {
            return await _service.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<UsuarioDTO>> Crear(UsuarioDTO entity, int userId)
        {
            return await _service.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<UsuarioDTO>> Eliminar(int id, int userId)
        {
            return await _service.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _service.EliminarMultiples(ids, userId);
        }
    }
}
