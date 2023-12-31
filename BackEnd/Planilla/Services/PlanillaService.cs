﻿using AutoMapper;
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
using OfficeOpenXml.Table;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;

namespace Planilla.Services
{
    public class PlanillaService : BaseService<EncabezadoPlanilla>
    {

        private readonly IMapper _mapper;
        private readonly DetallePlanillaService _detallePlanillaService;
        private readonly EmailService _emailService;
        private List<string> nombreMeses = new List<string>() { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        public PlanillaService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
            _detallePlanillaService = new DetallePlanillaService(context, appSettingsModule, mapper);
            _emailService = new EmailService(context,appSettingsModule,mapper);
        }

        public async Task<ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>> response = new ResponseWrapperDTO<IList<EncabezadoPlanillaDTO>>();
            try
            {
                var registros = await _dBContext.EncabezadoPlanilla.Include(y => y.EstadoPlanilla).Include(x => x.Periodo).OrderByDescending(x => x.Creado).ToListAsync();
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
                var registro = await _dBContext.EncabezadoPlanilla.Include(y => y.EstadoPlanilla).Include(x => x.Periodo).Where(c => c.EncabezadoPlanillaId == id).FirstOrDefaultAsync();
                if (registro != null)
                {
                    response.Data = _mapper.Map<EncabezadoPlanillaDTO>(registro);
                    var detalles = await _dBContext.DetallePlanilla.Include(y => y.Empleado).Where(x => x.EncabezadoPlanillaId == id).ToListAsync();
                    response.Data.DetallePlanilla = _mapper.Map<List<DetallePlanillaDTO>>(detalles);
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

        public async Task<ResponseWrapperDTO<bool>> ActualizarDTO(EncabezadoPlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<bool> response = new ResponseWrapperDTO<bool>();
            using (var transaction = await _dBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var estadoPlanilla = await _dBContext.EstadoPlanilla.Where(x => x.EstadoPlanillaId == registro.EstadoPlanillaId).FirstOrDefaultAsync();
                    var periodoPlanilla = await _dBContext.Periodo.Where(x => x.PeriodoId == registro.PeriodoId).FirstOrDefaultAsync();
                    if (periodoPlanilla != null)
                    {
                        EncabezadoPlanilla registroGuardar = _mapper.Map<EncabezadoPlanilla>(registro);

                        var nuevoEncabezadoData = await Actualizar(registroGuardar, userId);
                        var correosEnviados = false;
                        foreach (var detalle in registro.DetallePlanilla)
                        {
                            var empleado = await _dBContext.Empleado.Where(x => x.Codigo == detalle.CodigoEmpleado).FirstOrDefaultAsync();
                            if (empleado != null)
                            {
                                detalle.EmpleadoId = empleado.EmpleadoId;
                                detalle.EncabezadoPlanillaId = nuevoEncabezadoData.Data.EncabezadoPlanillaId;
                                DetallePlanilla detalleActualizar = _mapper.Map<DetallePlanilla>(detalle);
                                if (estadoPlanilla.Codigo == "APROBADO" && registroGuardar.CorreoEnviado == false)
                                {
                                    correosEnviados=true;
                                    await _emailService.EnviarEmailEmpleados(empleado, periodoPlanilla.FechaFin);

                                }
                            }
                            else
                            {
                                await transaction.RollbackAsync();
                                response.AddResponseStatus(2, "Bad Request", "Empleado no encontrado con código: " + detalle.CodigoEmpleado);
                            }
                        }
                        if(correosEnviados) { 
                                    nuevoEncabezadoData.Data.CorreoEnviado = true;
                        }
                        await transaction.CommitAsync();
                        response.Data = true;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                    LogError excepcion = (LogError)ex;
                    excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ActualizarDTO");
                    exceptionHandler.SaveException(excepcion);
                }
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<bool>> CrearDTO(EncabezadoPlanillaDTO registro, int userId)
        {
            ResponseWrapperDTO<bool> response = new ResponseWrapperDTO<bool>() { Data = false };
            using (var transaction = await _dBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var estadoPlanilla = await _dBContext.EstadoPlanilla.Where(x => x.EstadoPlanillaId == registro.EstadoPlanillaId).FirstOrDefaultAsync();
                    var periodoPlanilla = await _dBContext.Periodo.Where(x => x.PeriodoId == registro.PeriodoId).FirstOrDefaultAsync();
                    if (periodoPlanilla != null)
                    {
                        EncabezadoPlanilla registroGuardar = new EncabezadoPlanilla() { PeriodoId = registro.PeriodoId, EstadoPlanillaId = registro.EstadoPlanillaId, Descripcion = "Planilla hasta el " + periodoPlanilla.FechaFin.Value.Date.ToString("yyyy-MM-dd"), FechaCorte = periodoPlanilla.FechaFin.Value };
                        var nuevoEncabezadoData = await Crear(registroGuardar, userId);

                        foreach (var detalle in registro.DetallePlanilla)
                        {
                            var empleado = await _dBContext.Empleado.Where(x => x.Codigo == detalle.CodigoEmpleado).FirstOrDefaultAsync();
                            if (empleado != null)
                            {
                                detalle.EmpleadoId = empleado.EmpleadoId;
                                detalle.EncabezadoPlanillaId = nuevoEncabezadoData.Data.EncabezadoPlanillaId;
                                await _detallePlanillaService.CrearDTO(detalle, userId);
                                if (estadoPlanilla.Codigo == "APROBADO" && registroGuardar.CorreoEnviado == false)
                                {
                                    await _emailService.EnviarEmailEmpleados(empleado, periodoPlanilla.FechaFin);
                                    nuevoEncabezadoData.Data.CorreoEnviado= true;

                                }
                            }
                            else
                            {
                                await transaction.RollbackAsync();
                                response.AddResponseStatus(2, "Bad Request", "Empleado no encontrado con código: " + detalle.CodigoEmpleado);
                            }

                        }
                        await transaction.CommitAsync();
                        response.Data = true;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                    LogError excepcion = (LogError)ex;
                    excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "CrearDTO");
                    exceptionHandler.SaveException(excepcion);
                }
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<bool>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<bool> response = new ResponseWrapperDTO<bool>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Data = false;
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

        /// <summary>
        /// Obtiene base64 de plantilla excel generado para importar planilla
        /// </summary>
        /// <returns></returns>
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

                        var sheetName = await _dBContext.ConfiguracionGlobal.Where(x => x.Codigo == "SHEETEXCEL").FirstOrDefaultAsync();
                        var worksheet = package.Workbook.Worksheets.Add(sheetName.Valor);
                        foreach (var column in registros)
                        {

                            worksheet.Cells[1, columna].Value = column.Nombre;
                            worksheet.Cells[1, columna].AutoFitColumns();
                            columna++;
                        }
                        ExcelRange range = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
                        ExcelTable tab = worksheet.Tables.Add(range, "Planilla");
                        tab.TableStyle = TableStyles.Medium9;
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

        /// <summary>
        /// Obtiene datos iniciales para configurar una nueva planilla
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<EncabezadoPlanillaDTO>> ObtenerDatosIniciales()
        {

            ResponseWrapperDTO<EncabezadoPlanillaDTO> response = new ResponseWrapperDTO<EncabezadoPlanillaDTO>() { Data = new EncabezadoPlanillaDTO() };
            try
            {
                var estadoInicial = await _dBContext.EstadoPlanilla.Where(x => x.Codigo == "BORRADOR").FirstOrDefaultAsync();
                var periodoActivo = await _dBContext.Periodo.Where(x => x.Habilitado == true).OrderByDescending(x => x.PeriodoId).FirstOrDefaultAsync();
                response.Data.Descripcion = "DATOS INICIALES";
                response.Data.EstadoPlanillaId = estadoInicial != null ? estadoInicial.EstadoPlanillaId : -1;
                response.Data.PeriodoId = periodoActivo != null ? periodoActivo.PeriodoId : -1;
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

        /// <summary>
        /// Extrae informacion de excel y convierte a lista de DetallePlanillaDTO para visualizar en tabla editable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ResponseWrapperDTO<IList<DetallePlanillaDTO>> PrecargaExcel(IFormFile file)
        {

            ResponseWrapperDTO<IList<DetallePlanillaDTO>> response = new ResponseWrapperDTO<IList<DetallePlanillaDTO>>() { Data = new List<DetallePlanillaDTO>() };
            try
            {
                if (file != null)
                {
                    if (file.FileName.Contains(".xlsx") || file.FileName.Contains(".xls"))
                    {
                        using (var package = new ExcelPackage(file.OpenReadStream()))
                        {
                            var sheetName = _dBContext.ConfiguracionGlobal.Where(x => x.Codigo == "SHEETEXCEL").FirstOrDefault();
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(x => x.Name == sheetName.Valor).FirstOrDefault();
                            if (worksheet != null)
                            {
                                int cantFilas = worksheet.Dimension.Rows;
                                for (int row = 2; row <= cantFilas; row++)
                                {
                                    var nuevoDetalle = new DetallePlanillaDTO();
                                    nuevoDetalle.CodigoEmpleado = worksheet.Cells[row, 1] != null ? worksheet.Cells[row, 1].Value.ToString() : "";
                                    nuevoDetalle.Salario = worksheet.Cells[row, 2] != null ? Convert.ToDecimal(worksheet.Cells[row, 2].Value) : 0;
                                    nuevoDetalle.DescuentoIsss = worksheet.Cells[row, 3] != null ? Convert.ToDecimal(worksheet.Cells[row, 3].Value) : 0;
                                    nuevoDetalle.DescuentoAfp = worksheet.Cells[row, 4] != null ? Convert.ToDecimal(worksheet.Cells[row, 4].Value) : 0;
                                    nuevoDetalle.DescuentoRenta = worksheet.Cells[row, 5] != null ? Convert.ToDecimal(worksheet.Cells[row, 5].Value) : 0;
                                    nuevoDetalle.OtrosDescuentos = worksheet.Cells[row, 6] != null ? Convert.ToDecimal(worksheet.Cells[row, 6].Value) : 0;
                                    nuevoDetalle.SueldoNeto = worksheet.Cells[row, 7] != null ? Convert.ToDecimal(worksheet.Cells[row, 7].Value) : 0;
                                    response.Data.Add(nuevoDetalle);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "PrecargaExcel");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        /// <summary>
        /// Deshabilitar una planilla para que ya no sea mostrada al empleado sin la necesitad de elimnar la planilla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<bool>> DeshabilitarPlanilla(int id, int userId)
        {
            ResponseWrapperDTO<bool> response = new ResponseWrapperDTO<bool>();
            try
            {
                var planilla = await _dBContext.EncabezadoPlanilla.Where(x => x.EncabezadoPlanillaId == id).FirstOrDefaultAsync();
                if (planilla != null)
                {
                    planilla.Habilitado = !planilla.Habilitado;
                    await Actualizar(planilla, userId);
                }
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "DeshabilitarPlanilla");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        /// <summary>
        /// Obtiene los años en los que el empleado ha devengado al menos un salario
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<IList<int>>> ObtenerAnios()
        {

            ResponseWrapperDTO<IList<int>> response = new ResponseWrapperDTO<IList<int>>() { Data = new List<int>() };
            try
            {
                var planillas = await _dBContext.EncabezadoPlanilla.Select(x => x.Periodo.FechaFin.Value.Year).Distinct().OrderByDescending(x => x).ToListAsync();
                response.Data = planillas;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ObtenerAnios");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        /// <summary>
        /// Obtiene los meses en los que el empleado ha devengado al menos un salario
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<IList<object>>> ObtenerMesesPlanilla(int anio)
        {

            ResponseWrapperDTO<IList<object>> response = new ResponseWrapperDTO<IList<object>>() { Data = new List<object>() };
            try
            {
                var mesesPlanilla = await _dBContext.EncabezadoPlanilla.Where(x => x.Periodo.FechaFin.Value.Year == anio).Select(x => x.Periodo.FechaFin.Value.Month).Distinct().OrderByDescending(x => x).ToListAsync();
                foreach (var item in mesesPlanilla)
                {
                    response.Data.Add(ObtenerNombreMes(item));
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ObtenerMesesPlanilla");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        /// <summary>
        /// Obtiene lista resumen de boletas de pago de un empleado
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<IList<ResumenBoletaPagoDTO>>> ObtenerResumenBoletaEmpleado(int anio, int mes, int usuarioId)
        {

            ResponseWrapperDTO<IList<ResumenBoletaPagoDTO>> response = new ResponseWrapperDTO<IList<ResumenBoletaPagoDTO>>() { Data = new List<ResumenBoletaPagoDTO>() };
            try
            {
                var usuarioEmpleado = await _dBContext.Usuario.Where(x => x.UsuarioId == usuarioId).FirstOrDefaultAsync();
                if (usuarioEmpleado != null)
                {
                    var boletasPago = await _dBContext.DetallePlanilla
                        .Where(x => x.EncabezadoPlanilla.Periodo.FechaFin.Value.Year == anio && x.EncabezadoPlanilla.Periodo.FechaFin.Value.Month == mes && x.Empleado.EmpleadoId == usuarioEmpleado.EmpleadoId && x.EncabezadoPlanilla.Habilitado == true).Include(x => x.EncabezadoPlanilla.Periodo).Distinct().OrderByDescending(x => x).ToListAsync();
                    response.Data = _mapper.Map<IList<ResumenBoletaPagoDTO>>(boletasPago);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ObtenerMesesPlanilla");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        /// <summary>
        /// Obtiene el detalle de una boleta de pago de un empleado
        /// </summary>
        /// <param name="detallePlanillaId"></param>
        /// <returns></returns>
        public async Task<ResponseWrapperDTO<BoletaPagoDTO>> ObtenerDetalleBoletaPago(int detallePlanillaId)
        {
            ResponseWrapperDTO<BoletaPagoDTO> response = new ResponseWrapperDTO<BoletaPagoDTO>() { Data = new BoletaPagoDTO() };
            try
            {
                var boletaPago = await _dBContext.DetallePlanilla
                    .Where(x => x.DetallePlanillaId == detallePlanillaId).Include(x => x.Empleado.Puesto).Include(x => x.EncabezadoPlanilla.Periodo).FirstOrDefaultAsync();
                response.Data = _mapper.Map<BoletaPagoDTO>(boletaPago);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("PlanillaService", "ObtenerMesesPlanilla");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        private object ObtenerNombreMes(int mes)
        {
            return new { Code = mes, Name = nombreMeses[mes - 1] };

        }

    }
}
