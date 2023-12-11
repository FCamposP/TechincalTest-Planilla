using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using Planilla.Entities;
using Planilla.Services.LogCustom;
using Planilla.DataAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Planilla.Services
{
    public class BaseService<T> : IService<T> where T : class
    {
        public ApiDBContext _dBContext;
        protected readonly DbSet<T> _items;
        public ExceptionLogHandler exceptionHandler;
        protected readonly EventLogHandler eventLogHandler;
        public BaseService(ApiDBContext context, IAppSettingsModule appSettingsModule)
        {
            _dBContext = context;
            _items = context.Set<T>();
            exceptionHandler = new ExceptionLogHandler(context, appSettingsModule);
            eventLogHandler = new EventLogHandler(context, appSettingsModule);
        }

        public async Task<ResponseWrapperDTO<IList<T>>> GetAll()
        {
            ResponseWrapperDTO<IList<T>> response = new ResponseWrapperDTO<IList<T>>();
            try
            {
                response.Data = await _items.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, al obtener todos los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "GetAll");
                exceptionHandler.SaveException(excepcion);
            }
            finally
            {
                LogEvento eventLog = new LogEvento();
                //eventLogHandler.SaveLog(eventLog);
            }
            return response;
        }


        public async Task<ResponseWrapperDTO<T>> GetById(int id)
        {
            ResponseWrapperDTO<T> response = new ResponseWrapperDTO<T>();
            try
            {
                response.Data = await _items.FindAsync(id);

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al obtener el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "GetById");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<T>> Crear(T entity, int userId)
        {
            ResponseWrapperDTO<T> response = new ResponseWrapperDTO<T>();
            try
            {
                GuardarPropiedad("Creador", entity, userId);
                GuardarPropiedad("Creado", entity, DateTime.Now);
                GuardarPropiedad("Activo", entity, true);
                _items.Add(entity);
                await _dBContext.SaveChangesAsync();
                response.Data = entity;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Crear");
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<T>> CrearLog(T entity)
        {
            ResponseWrapperDTO<T> response = new ResponseWrapperDTO<T>();
            try
            {
                _items.Add(entity);
                await _dBContext.SaveChangesAsync();
                response.Data = entity;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Crear");
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<T>> Actualizar(T entidad, int userId)
        {
            ResponseWrapperDTO<T> response = new ResponseWrapperDTO<T>();
            try
            {
                GuardarPropiedad("Modificador", entidad, userId);
                GuardarPropiedad("Modificado", entidad, DateTime.Now);
                _dBContext.Update<T>(entidad);
                await _dBContext.SaveChangesAsync();
                response.Data = entidad;
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
        public async Task<ResponseWrapperDTO<T>> Eliminar(int id, int userId)
        {
            ResponseWrapperDTO<T> response = new ResponseWrapperDTO<T>();
            try
            {
                if (id > 0 && userId > 0)
                {
                    var objetoEliminar = _items.Find(id);
                    if (objetoEliminar != null)
                    {
                        GuardarPropiedad("Activo", objetoEliminar, false);

                        response = await Actualizar(objetoEliminar, userId);
                    }
                }
                else
                {
                    response.AddRequestStatus(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al eliminar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Eliminar");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        private void GuardarPropiedad(string propiedad, object objeto, object valor)
        {
            Type type = typeof(T);
            PropertyInfo? propertyInfo = type.GetProperty(propiedad);
            propertyInfo?.SetValue(objeto, valor, null);
        }
    }
}
