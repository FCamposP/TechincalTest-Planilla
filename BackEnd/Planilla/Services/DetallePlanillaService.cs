using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using Planilla.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Planilla.DTO.Planilla;

namespace Planilla.Services
{
    public class DetallePlanillaService : BaseService<DetallePlanilla>
    {
        private readonly IMapper _mapper;

        public DetallePlanillaService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<DetallePlanillaDTO>>> GetByPlanillaId(int planillaId)
        {
            ResponseWrapperDTO<IList<DetallePlanillaDTO>> response = new ResponseWrapperDTO<IList<DetallePlanillaDTO>>();
            try
            {
                var registros = await _dBContext.DetallePlanilla.Where(x=>x.EncabezadoPlanillaId==planillaId).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<DetallePlanillaDTO>>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("DetallePlanillaService", "GetByPlanillaId");
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<DetallePlanillaDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<DetallePlanillaDTO> response = new ResponseWrapperDTO<DetallePlanillaDTO>();
            try
            {
                var registros = await _dBContext.DetallePlanilla.Where(c => c.DetallePlanillaId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<DetallePlanillaDTO>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<DetallePlanillaDTO>> ActualizarDTO(DetallePlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<DetallePlanillaDTO> response = new ResponseWrapperDTO<DetallePlanillaDTO>();
            try
            {
                DetallePlanilla registroGuardar = new DetallePlanilla();
                registroGuardar = _mapper.Map<DetallePlanillaDTO, DetallePlanilla>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<DetallePlanilla, DetallePlanillaDTO>(result.Data ?? new DetallePlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("DetallePlanillaService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<DetallePlanillaDTO>> CrearDTO(DetallePlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<DetallePlanillaDTO> response = new ResponseWrapperDTO<DetallePlanillaDTO>();
            try
            {
                DetallePlanilla registroGuardar = new DetallePlanilla();
                registroGuardar = _mapper.Map<DetallePlanillaDTO, DetallePlanilla>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<DetallePlanilla, DetallePlanillaDTO>(result.Data ?? new DetallePlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("DetallePlanillaService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<DetallePlanillaDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<DetallePlanillaDTO> response = new ResponseWrapperDTO<DetallePlanillaDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<DetallePlanilla, DetallePlanillaDTO>(result.Data ?? new DetallePlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("DetallePlanillaService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            ResponseWrapperDTO<int> response = new ResponseWrapperDTO<int>();
            try
            {
                foreach (int id in ids)
                {
                    var result = await Eliminar(id, userId);
                    if (result != null)
                    {
                        response.Data++;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron eliminar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

    }

}
