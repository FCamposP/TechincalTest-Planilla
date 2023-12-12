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
    public class PeriodoController : ControllerBaseCustom
    {
        readonly protected PeriodoService periodoService;

        public PeriodoController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            periodoService = new PeriodoService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<PeriodoDTO>>> Get()
        {
            return await periodoService.GetAllDTO();
        }

        [Route("GetAllDTOQEQ")]
        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<PeriodoDTO>>> GetAllDTOQEQ()
        {
            return await periodoService.GetAllDTOQEQ();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<PeriodoDTO>> GetById(int id)
        {
            return await periodoService.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<PeriodoDTO>> Actualizar(PeriodoDTO entity, int userId)
        {
            return await periodoService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<PeriodoDTO>> Crear(PeriodoDTO entity, int userId)
        {
            return await periodoService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<PeriodoDTO>> Eliminar(int id, int userId)
        {
            return await periodoService.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await periodoService.EliminarMultiples(ids, userId);
        }

    }
}
