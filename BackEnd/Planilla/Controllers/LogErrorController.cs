using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planilla.DataAccess;
using Planilla.Services;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using Planilla.DTO.LogError;

namespace Planilla.Controllers
{
    public class LogErrorController : ControllerBaseCustom
    {
        readonly protected LogErrorService _erroLogService;

        public LogErrorController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _erroLogService = new LogErrorService(context, appSettingsModule, mapper);
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<IList<LogErrorSimpleDTO>>> ObtenerErroresPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _erroLogService.ObtenerListaErrores(fechaInicio, fechaFin);
        }
        
        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<LogErrorDTO>> ObtenerDetalleError(int LogErrorId)
        {
            return await _erroLogService.ObtenerDetalleError(LogErrorId);
        }
    }
}
