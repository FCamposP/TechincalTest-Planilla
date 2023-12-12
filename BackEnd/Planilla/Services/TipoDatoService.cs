using AutoMapper;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using Planilla.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Planilla.Services
{
    public class TipoDatoService : BaseService<TipoDato>
    {

        private readonly IMapper _mapper;

        public TipoDatoService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<TipoDatoDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<TipoDatoDTO>> response = new ResponseWrapperDTO<IList<TipoDatoDTO>>();
            try
            {
                var registros = await _dBContext.TipoDato.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<TipoDatoDTO>>(registros);
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

        public async Task<ResponseWrapperDTO<TipoDatoDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<TipoDatoDTO> response = new ResponseWrapperDTO<TipoDatoDTO>();
            try
            {
                var registros = await _dBContext.TipoDato.Where(c => c.TipoDatoId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<TipoDatoDTO>(registros);
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

        public async Task<ResponseWrapperDTO<TipoDatoDTO>> ActualizarDTO(TipoDatoDTO registro, int userId)
        {
            ResponseWrapperDTO<TipoDatoDTO> response = new ResponseWrapperDTO<TipoDatoDTO>();
            try
            {
                TipoDato registroGuardar = new TipoDato();
                registroGuardar = _mapper.Map<TipoDatoDTO, TipoDato>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<TipoDato, TipoDatoDTO>(result.Data ?? new TipoDato());
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

        public async Task<ResponseWrapperDTO<TipoDatoDTO>> CrearDTO(TipoDatoDTO registro, int userId)
        {
            ResponseWrapperDTO<TipoDatoDTO> response = new ResponseWrapperDTO<TipoDatoDTO>();
            try
            {
                TipoDato registroGuardar = new TipoDato();
                registroGuardar = _mapper.Map<TipoDatoDTO, TipoDato>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<TipoDato, TipoDatoDTO>(result.Data ?? new TipoDato());
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

        public async Task<ResponseWrapperDTO<TipoDatoDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<TipoDatoDTO> response = new ResponseWrapperDTO<TipoDatoDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<TipoDato, TipoDatoDTO>(result.Data ?? new TipoDato());
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
