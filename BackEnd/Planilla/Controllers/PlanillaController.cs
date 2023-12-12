using AutoMapper;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using QEQBACK.Back.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planilla.Services;
using Planilla.DTO.Planilla;

namespace Planilla.Controllers
{
    public class PlanillaController : ControllerBaseCustom
    {
        readonly protected PlanillaService _planillaService;

        public PlanillaController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _planillaService = new PlanillaService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>>> Get()
        {
            return await _planillaService.GetAllDTO();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> GetById(int id)
        {
            return await _planillaService.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> Actualizar(EncabezadoPlanillaDTO entity, int userId)
        {
            return await _planillaService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> Crear(EncabezadoPlanillaDTO entity, int userId)
        {
            return await _planillaService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> Eliminar(int id, int userId)
        {
            return await _planillaService.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _planillaService.EliminarMultiples(ids, userId);
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<object>> DescargarPlantilla()
        {
            ResponseWrapperDTO<object> response = new ResponseWrapperDTO<object>();
              var result  = await _planillaService.DescargarPlantilla();
            if (result.Data != null)
            {
                response.Data = new {Archivo= result .Data, Nombre= "Plantilla-Planilla.xlsx" };
            }
            return response;
        }
    }
}
