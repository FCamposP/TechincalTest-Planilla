using AutoMapper;
using Planilla.Entities;
using Planilla.Utilities;
using System;
using System.Text;
using Planilla.DataAccess;
using Planilla.Abstractions;

namespace Planilla.Services.LogCustom
{
    public class ExceptionLogHandler
    {
        private readonly ErrorService LogErrorsService = new ErrorService();
        private readonly ApiDBContext _dbContext;
        private readonly IAppSettingsModule _appSettingsModule;

        public ExceptionLogHandler(ApiDBContext dBContext,IAppSettingsModule appSettingsModule)
        {
            _dbContext = dBContext;
            _appSettingsModule = appSettingsModule;
        }

        public void SaveException(Exception ex)
        {
            SaveException((LogError)ex);
        }
        public void SaveException(LogError ex)
        {
            LogErrorsService.Logger = new DBLogErrorger(_dbContext, _appSettingsModule);
            LogErrorsService.RegisterException(ex);
            //if (!LogErrorsService.RegisterException(ex))
            //{
            //    LogErrorsService.Logger = new FileLogErrorger();
            //    LogErrorsService.RegisterException(ex);
            //}
        }

        public string InformacionAdicionalMetodo(string archivo, string metodo, string adicional = "")
        {
            return "Archivo: " + archivo + " - Método: " + metodo + ". Adicional: " + adicional;
        }
    }
}