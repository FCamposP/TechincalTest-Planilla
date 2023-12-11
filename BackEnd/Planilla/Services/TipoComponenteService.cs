using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planilla.DataAccess;
using Planilla.Abstractions;

namespace Planilla.Services
{
    public class TipoComponenteService : BaseService<TipoComponente>
    {
        private readonly IMapper _mapper;

        public TipoComponenteService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<TipoComponenteDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<TipoComponenteDTO>> response = new ResponseWrapperDTO<IList<TipoComponenteDTO>>();
            try
            {
                var registros = await _dBContext.TipoComponente.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<TipoComponenteDTO>>(registros);
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

        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<TipoComponenteDTO> response = new ResponseWrapperDTO<TipoComponenteDTO>();
            try
            {
                var registros = await _dBContext.TipoComponente.Where(c => c.TipoComponenteId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<TipoComponenteDTO>(registros);
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

        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> ActualizarDTO(TipoComponenteDTO registro, int userId)
        {
            ResponseWrapperDTO<TipoComponenteDTO> response = new ResponseWrapperDTO<TipoComponenteDTO>();
            try
            {
                TipoComponente registroGuardar = new TipoComponente();
                registroGuardar = _mapper.Map<TipoComponenteDTO, TipoComponente>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<TipoComponente, TipoComponenteDTO>(result.Data ?? new TipoComponente());
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

        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> CrearDTO(TipoComponenteDTO registro, int userId)
        {
            ResponseWrapperDTO<TipoComponenteDTO> response = new ResponseWrapperDTO<TipoComponenteDTO>();
            try
            {
                TipoComponente registroGuardar = new TipoComponente();
                registroGuardar = _mapper.Map<TipoComponenteDTO, TipoComponente>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<TipoComponente, TipoComponenteDTO>(result.Data ?? new TipoComponente());
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

        public async Task<ResponseWrapperDTO<TipoComponenteDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<TipoComponenteDTO> response = new ResponseWrapperDTO<TipoComponenteDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<TipoComponente, TipoComponenteDTO>(result.Data ?? new TipoComponente());
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
