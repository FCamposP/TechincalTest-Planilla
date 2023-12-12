using Planilla.Entities;
using Microsoft.EntityFrameworkCore;
using Planilla.DTO.Others;
using AutoMapper;
using System.Data;
using Planilla.DTO.Componente;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Planilla.DataAccess;
using Planilla.Abstractions;
using Planilla.DTO;
using Planilla.Services;

namespace QEQBACK.Back.Services
{
    public class PuestoService : BaseService<Puesto>
    {

        private readonly IMapper _mapper;

        public PuestoService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<PuestoDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<PuestoDTO>> response = new ResponseWrapperDTO<IList<PuestoDTO>>();
            try
            {
                var registros = await _dBContext.Puesto.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<PuestoDTO>>(registros);
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

        public async Task<ResponseWrapperDTO<PuestoDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<PuestoDTO> response = new ResponseWrapperDTO<PuestoDTO>();
            try
            {
                var registros = await _dBContext.Puesto.Where(c => c.PuestoId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<PuestoDTO>(registros);
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

        public async Task<ResponseWrapperDTO<PuestoDTO>> ActualizarDTO(PuestoDTO registro, int userId)
        {
            ResponseWrapperDTO<PuestoDTO> response = new ResponseWrapperDTO<PuestoDTO>();
            try
            {
                Puesto registroGuardar = new Puesto();
                registroGuardar = _mapper.Map<PuestoDTO, Puesto>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<Puesto, PuestoDTO>(result.Data ?? new Puesto());
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

        public async Task<ResponseWrapperDTO<PuestoDTO>> CrearDTO(PuestoDTO registro, int userId)
        {
            ResponseWrapperDTO<PuestoDTO> response = new ResponseWrapperDTO<PuestoDTO>();
            try
            {
                Puesto registroGuardar = new Puesto();
                registroGuardar = _mapper.Map<PuestoDTO, Puesto>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<Puesto, PuestoDTO>(result.Data ?? new Puesto());
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

        public async Task<ResponseWrapperDTO<PuestoDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<PuestoDTO> response = new ResponseWrapperDTO<PuestoDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<Puesto, PuestoDTO>(result.Data ?? new Puesto());
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
