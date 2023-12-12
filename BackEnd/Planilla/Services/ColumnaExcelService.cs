using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Planilla.DTO;

namespace Planilla.Services
{
    public class ColumnaExcelService : BaseService<ColumnaExcel>
    {
        private readonly IMapper _mapper;

        public ColumnaExcelService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<ColumnaExcelDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<ColumnaExcelDTO>> response = new ResponseWrapperDTO<IList<ColumnaExcelDTO>>();
            try
            {
                var registros = await _dBContext.ColumnaExcel.Include(x => x.TipoDato).OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<ColumnaExcelDTO>>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "GetAllDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> GetByIdDTO(int id)
        {
            ResponseWrapperDTO<ColumnaExcelDTO> response = new ResponseWrapperDTO<ColumnaExcelDTO>();
            try
            {
                var c = await _dBContext.ColumnaExcel.Where(x => x.ColumnaExcelId == id).FirstOrDefaultAsync();
                if (c != null)
                {
                    response.Data = _mapper.Map<ColumnaExcel, ColumnaExcelDTO>(c);

                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "GetByIdDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> ActualizarDTO(ColumnaExcelDTO registro, int userId)
        {
            ResponseWrapperDTO<ColumnaExcelDTO> response = new ResponseWrapperDTO<ColumnaExcelDTO>();
            try
            {
                ColumnaExcel registroGuardar = new ColumnaExcel();
                registroGuardar = _mapper.Map<ColumnaExcelDTO, ColumnaExcel>(registro);
                var result = await Actualizar(registroGuardar, userId);
                response.Data = _mapper.Map<ColumnaExcel, ColumnaExcelDTO>(result.Data ?? new ColumnaExcel());


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "ActualizarDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> CrearDTO(ColumnaExcelDTO registro, int userId)
        {
            ResponseWrapperDTO<ColumnaExcelDTO> response = new ResponseWrapperDTO<ColumnaExcelDTO>();
            try
            {
                ColumnaExcel registroGuardar = new ColumnaExcel();
                registroGuardar = _mapper.Map<ColumnaExcelDTO, ColumnaExcel>(registro);
                var result = await Crear(registroGuardar, userId);
                response.Data = _mapper.Map<ColumnaExcel, ColumnaExcelDTO>(result.Data ?? new ColumnaExcel());

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "CrearDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ColumnaExcelDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<ColumnaExcelDTO> response = new ResponseWrapperDTO<ColumnaExcelDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<ColumnaExcel, ColumnaExcelDTO>(result.Data ?? new ColumnaExcel());

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "EliminarDTO");
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
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("ColumnaExcelService", "EliminarMultiples");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }
    }
}
