using Planilla.DTO;
using Planilla.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;

namespace Planilla.Services
{
    public class EmpleadoService : BaseService<Empleado>
    {
        private readonly IMapper _mapper;

        public EmpleadoService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<EmpleadoDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<EmpleadoDTO>> response = new ResponseWrapperDTO<IList<EmpleadoDTO>>();
            try
            {
                var registros = await _dBContext.Empleado.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<EmpleadoDTO>>(registros);
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

        public async Task<ResponseWrapperDTO<EmpleadoDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<EmpleadoDTO> response = new ResponseWrapperDTO<EmpleadoDTO>();
            try
            {
                var registros = await _dBContext.Empleado.Where(c => c.EmpleadoId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<EmpleadoDTO>(registros);
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

        public async Task<ResponseWrapperDTO<EmpleadoDTO>> ActualizarDTO(EmpleadoDTO registro, int userId)
        {
            ResponseWrapperDTO<EmpleadoDTO> response = new ResponseWrapperDTO<EmpleadoDTO>();
            try
            {
                Empleado registroGuardar = new Empleado();
                registroGuardar = _mapper.Map<EmpleadoDTO, Empleado>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<Empleado, EmpleadoDTO>(result.Data ?? new Empleado());
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

        public async Task<ResponseWrapperDTO<EmpleadoDTO>> CrearDTO(EmpleadoDTO registro, int userId)
        {
            ResponseWrapperDTO<EmpleadoDTO> response = new ResponseWrapperDTO<EmpleadoDTO>();
            try
            {
                Empleado registroGuardar = new Empleado();
                registroGuardar = _mapper.Map<EmpleadoDTO, Empleado>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<Empleado, EmpleadoDTO>(result.Data ?? new Empleado());
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

        public async Task<ResponseWrapperDTO<EmpleadoDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<EmpleadoDTO> response = new ResponseWrapperDTO<EmpleadoDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<Empleado, EmpleadoDTO>(result.Data ?? new Empleado());
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
