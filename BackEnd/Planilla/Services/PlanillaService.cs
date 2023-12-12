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
using Planilla.DTO.Planilla;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Planilla.Services
{
    public class PlanillaService : BaseService<EncabezadoPlanilla>
    {

        private readonly IMapper _mapper;

        public PlanillaService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>> response = new ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>>();
            try
            {
                var registros = await _dBContext.EncabezadoPlanilla.OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<EncabezadoPlanillaDTO>>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "GetAllDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> GetByIdDTO(int id)
        {

            ResponseWrapperDTO<EncabezadoPlanillaDTO> response = new ResponseWrapperDTO<EncabezadoPlanillaDTO>();
            try
            {
                var registros = await _dBContext.EncabezadoPlanilla.Where(c => c.EncabezadoPlanillaId == id).FirstOrDefaultAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<EncabezadoPlanillaDTO>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "GetByIdDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<byte[]>> DescargarPlantilla()
        {

            ResponseWrapperDTO<byte[]> response = new ResponseWrapperDTO<byte[]>();
            try
            {
                var registros = await _dBContext.ColumnaExcel.OrderBy(x => x.Orden).ToListAsync();
                if (registros != null)
                {
                    int columna = 1;
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Planilla de Pagos");
                        foreach (var column in registros)
                        {

                            worksheet.Cells[1, columna].Value = column.Nombre;
                            columna++;
                        }
                        response.Data = package.GetAsByteArray();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "DescargarPlantilla");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> ActualizarDTO(EncabezadoPlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<EncabezadoPlanillaDTO> response = new ResponseWrapperDTO<EncabezadoPlanillaDTO>();
            try
            {
                EncabezadoPlanilla registroGuardar = new EncabezadoPlanilla();
                registroGuardar = _mapper.Map<EncabezadoPlanillaDTO, EncabezadoPlanilla>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<EncabezadoPlanilla, EncabezadoPlanillaDTO>(result.Data ?? new EncabezadoPlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ActualizarDTO");
                exceptionHandler.SaveException(excepcion);
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> CrearDTO(EncabezadoPlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<EncabezadoPlanillaDTO> response = new ResponseWrapperDTO<EncabezadoPlanillaDTO>();
            try
            {
                EncabezadoPlanilla registroGuardar = new EncabezadoPlanilla();
                registroGuardar = _mapper.Map<EncabezadoPlanillaDTO, EncabezadoPlanilla>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<EncabezadoPlanilla, EncabezadoPlanillaDTO>(result.Data ?? new EncabezadoPlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "CrearDTO");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<EncabezadoPlanillaDTO> response = new ResponseWrapperDTO<EncabezadoPlanillaDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<EncabezadoPlanilla, EncabezadoPlanillaDTO>(result.Data ?? new EncabezadoPlanilla());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "EliminarDTO");
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
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "EliminarMultiples");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

    }
}
