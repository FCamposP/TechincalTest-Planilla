using AutoMapper;
using Planilla.Abstractions;
using Planilla.Entities;
using Planilla.DataAccess;
using System;
using System.Net;

namespace Planilla.Services.LogCustom
{
    public class DBLogErrorger : ILogErrorger
    {
        private readonly BaseService<LogError> _dbLogService;
        private readonly IAppSettingsModule _appSettingsModule;

        public DBLogErrorger(ApiDBContext dBContext,IAppSettingsModule appSettingsModule)
        {
            _dbLogService = new BaseService<LogError>(dBContext, appSettingsModule);
            _appSettingsModule = appSettingsModule;
        }

        public void RegisterException(Exception ex)
        {
            LogError value = (LogError)ex;
            RegisterException(value);
        }

        public  void RegisterException(LogError ex)
        {
            ex.Creado = DateTime.Now;
            ex.Modulo = _appSettingsModule.ObtenerCodigoModulo();
            ex.Entorno = _appSettingsModule.ObtenerCodigoEntorno();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            String IP = Convert.ToString(localIPs[1]);
            var response= _dbLogService.CrearLog(ex);

        }
    }
}
