using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO;
using Planilla.DTO.Componente;
using Planilla.DTO.Others;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{

    public class ColumnaExcelController : ControllerBaseCustom
    {
        readonly protected ColumnaExcelService _service;
        public ColumnaExcelController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new ColumnaExcelService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<ColumnaExcelDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> GetById(int id)
        {
            return await _service.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> Actualizar(ColumnaExcelDTO entity, int userId)
        {
            return await _service.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> Crear(ColumnaExcelDTO entity, int userId)
        {
            return await _service.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> Eliminar(int id, int userId)
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
