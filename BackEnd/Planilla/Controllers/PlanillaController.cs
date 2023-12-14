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
using Microsoft.AspNetCore.Http;

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
        public async Task<ResponseWrapperDTO<bool>> Actualizar(EncabezadoPlanillaDTO entity, int userId)
        {
            return await _planillaService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<bool>> Crear(EncabezadoPlanillaDTO entity, int userId)
        {
            return await _planillaService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<bool>> Eliminar(int id, int userId)
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
            var result = await _planillaService.DescargarPlantilla();
            if (result.Data != null)
            {
                response.Data = new { Archivo = result.Data, Nombre = "Plantilla-Planilla.xlsx" };
            }
            return response;
        }


        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> ObtenerDatosIniciales()
        {
            return await _planillaService.ObtenerDatosIniciales();
        }

        [HttpPost("[action]")]
        public async Task<ResponseWrapperDTO<bool>> DeshabilitarPlanilla(int id, int userId)
        {
            return await _planillaService.DeshabilitarPlanilla(id,userId);
        }


        [HttpPost]
        [Route("[action]")]
        public ResponseWrapperDTO<IList<DetallePlanillaDTO>> PrecargaExcel()
        {
            return _planillaService.PrecargaExcel(Request.Form.Files[0]);
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<IList<int>>> ObtenerAniosPlanilla()
        {
            return await _planillaService.ObtenerAnios();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<IList<object>>> ObtenerMesesPlanilla(int anio)
        {
            return await _planillaService.ObtenerMesesPlanilla(anio);
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<IList<ResumenBoletaPagoDTO>>> ObtenerResumenBoletaEmpleado(int anio, int mes, int userId)
        {
            return await _planillaService.ObtenerResumenBoletaEmpleado(anio,mes,userId);
        }
        
        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<BoletaPagoDTO>> ObtenerDetalleBoletaPago(int detallePlanillaId)
        {
            return await _planillaService.ObtenerDetalleBoletaPago(detallePlanillaId);
        }
    }
}
