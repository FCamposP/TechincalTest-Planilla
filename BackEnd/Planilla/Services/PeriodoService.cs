using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Planilla.DTO;
using System.Linq;

namespace Planilla.Services
{
    public class PeriodoService : BaseService<Periodo>
    {

        private readonly IMapper _mapper;

        public PeriodoService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<PeriodoDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<PeriodoDTO>> response = new ResponseWrapperDTO<IList<PeriodoDTO>>();
            try
            {
                var registros = await _dBContext.Periodo.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<PeriodoDTO>>(registros);
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

        public async Task<ResponseWrapperDTO<IList<PeriodoDTO>>> GetAllDTOQEQ()
        {
            ResponseWrapperDTO<IList<PeriodoDTO>> response = new ResponseWrapperDTO<IList<PeriodoDTO>>();
            try
            {
                var registros = await _dBContext.Periodo.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<PeriodoDTO>>(registros);
                    foreach (var item in response.Data)
                    {
                        item.Descripcion = "Año - " + item.FechaFin?.Year.ToString();
                    }
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

        public async Task<ResponseWrapperDTO<PeriodoDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<PeriodoDTO> response = new ResponseWrapperDTO<PeriodoDTO>();
            try
            {
                var registros = await _dBContext.Periodo.Where(c => c.PeriodoId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<PeriodoDTO>(registros);
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

        public async Task<ResponseWrapperDTO<PeriodoDTO>> ActualizarDTO(PeriodoDTO registro, int userId)
        {
            ResponseWrapperDTO<PeriodoDTO> response = new ResponseWrapperDTO<PeriodoDTO>();
            try
            {
                Periodo registroGuardar = new Periodo();
                registroGuardar = _mapper.Map<PeriodoDTO, Periodo>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<Periodo, PeriodoDTO>(result.Data ?? new Periodo());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<PeriodoDTO>> CrearDTO(PeriodoDTO registro, int userId)
        {
            ResponseWrapperDTO<PeriodoDTO> response = new ResponseWrapperDTO<PeriodoDTO>();
            try
            {
                Periodo registroGuardar = new Periodo();
                registroGuardar = _mapper.Map<PeriodoDTO, Periodo>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<Periodo, PeriodoDTO>(result.Data ?? new Periodo());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<PeriodoDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<PeriodoDTO> response = new ResponseWrapperDTO<PeriodoDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<Periodo, PeriodoDTO>(result.Data ?? new Periodo());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
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
