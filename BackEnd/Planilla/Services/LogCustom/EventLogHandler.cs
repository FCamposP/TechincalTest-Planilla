using AutoMapper;
using Planilla.DTO;
using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planilla.DataAccess;
using Planilla.Abstractions;

namespace Planilla.Services.LogCustom
{
    public class EventLogHandler
    {
        private readonly EventLogsService LogErrorsService = new EventLogsService();
        private readonly ApiDBContext _dbContext;
        IAppSettingsModule _appSettingsModule;
        public EventLogHandler(ApiDBContext dBContext, IAppSettingsModule appSettingsModule)
        {
            _dbContext = dBContext;
            _appSettingsModule = appSettingsModule;
        }
        public void SaveLog(LogEvento ex)
        {
            LogErrorsService.Logger = new DBEventLogger(_dbContext,_appSettingsModule);
            if (!LogErrorsService.RegisterException(ex))
            {
                LogErrorsService.RegisterException(ex);
            }
        }
    }
}
