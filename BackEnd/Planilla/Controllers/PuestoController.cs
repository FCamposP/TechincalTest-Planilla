using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using QEQBACK.Back.Services;

namespace Planilla.Controllers
{
    public class PuestoController : ControllerBaseCustom
    {
        readonly protected PuestoService _puestoService;

        public PuestoController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _puestoService = new PuestoService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<PuestoDTO>>> Get()
        {
            return await _puestoService.GetAllDTO();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<PuestoDTO>> GetById(int id)
        {
            return await _puestoService.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<PuestoDTO>> Actualizar(PuestoDTO entity, int userId)
        {
            return await _puestoService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<PuestoDTO>> Crear(PuestoDTO entity, int userId)
        {
            return await _puestoService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<PuestoDTO>> Eliminar(int id, int userId)
        {
            return await _puestoService.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _puestoService.EliminarMultiples(ids, userId);
        }
    }
}
