using AutoMapper;
using Planilla.DTO;
using Planilla.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Planilla.DTO.LogError;
using Planilla.DataAccess;
using Planilla.Abstractions;
using Planilla.DTO.Others;

namespace Planilla.Services
{
    public class LogErrorService : BaseService<LogErrorDTO>
    {
        private readonly IMapper _mapper;
        public LogErrorService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<LogErrorSimpleDTO>>> ObtenerListaErrores(DateTime fechaInicio, DateTime fechaFin)
        {
            ResponseWrapperDTO<IList<LogErrorSimpleDTO>> response = new ResponseWrapperDTO<IList<LogErrorSimpleDTO>>();
            try
            {
                var errores = await _dBContext.LogError.Where(x => x.Creado.Date >= fechaInicio.Date && x.Creado.Date <= fechaFin.Date).Distinct().OrderByDescending(x => x.Creado).ToListAsync();
                if (errores != null)
                    response.Data = _mapper.Map<List<LogErrorSimpleDTO>>(errores);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(0, "Ocurrió un error al intentar obtener el listado de errores.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("LogErrorsService", "ObtenerListaErrores");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<LogErrorDTO>> ObtenerDetalleError(int erroLogId)
        {
            ResponseWrapperDTO<LogErrorDTO> response = new ResponseWrapperDTO<LogErrorDTO>();
            try
            {
                var error = await _dBContext.LogError.Where(x => x.LogErrorId== erroLogId).FirstOrDefaultAsync();
                if (error != null)
                    response.Data = _mapper.Map<LogErrorDTO>(error);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(0, "Ocurrió un error al intentar obtener el detalle del error con id: "+erroLogId, ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("LogErrorsService", "ObtenerDetalleError");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }
    }
}
