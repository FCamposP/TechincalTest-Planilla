using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Componente;
using Planilla.DTO.Others;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class ComponenteController : ControllerBaseCustom
    {
        readonly protected ComponenteService _service;
        public ComponenteController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new ComponenteService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<ComponenteDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<ComponenteDTO>> GetById(int id)
        {
            return await _service.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<ComponenteDTO>> Actualizar(ComponenteDTO entity, int userId)
        {
            return await _service.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<ComponenteDTO>> Crear(ComponenteDTO entity, int userId)
        {
            return await _service.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<ComponenteDTO>> Eliminar(int id, int userId)
        {
            return await _service.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _service.EliminarMultiples(ids, userId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<List<ComponenteNavigation>>> ObtenerNavegacion(int userId)
        {
            return await _service.ObtenerNavegacion(userId);
        }
    }
}
