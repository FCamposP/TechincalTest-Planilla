using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class TipoComponenteController : ControllerBaseCustom
    {
        readonly protected TipoComponenteService _service;
        public TipoComponenteController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new TipoComponenteService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<TipoComponenteDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> GetById(int id)
        {
            return await _service.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> Actualizar(TipoComponenteDTO entity, int userId)
        {
            return await _service.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> Crear(TipoComponenteDTO entity, int userId)
        {
            return await _service.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> Eliminar(int id, int userId)
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
