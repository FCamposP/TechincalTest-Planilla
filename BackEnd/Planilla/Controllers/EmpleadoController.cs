using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Services;
using Planilla.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class EmpleadoController : ControllerBaseCustom
    {
        readonly protected EmpleadoService _service;
        public EmpleadoController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new EmpleadoService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<EmpleadoDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<EmpleadoDTO>> GetById(int id)
        {
            return await _service.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<EmpleadoDTO>> Actualizar(EmpleadoDTO entity, int userId)
        {
            return await _service.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<EmpleadoDTO>> Crear(EmpleadoDTO entity, int userId)
        {
            return await _service.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<EmpleadoDTO>> Eliminar(int id, int userId)
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
