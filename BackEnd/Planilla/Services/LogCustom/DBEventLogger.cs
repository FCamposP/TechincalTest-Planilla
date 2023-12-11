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
    public class DBEventLogger : IEventLogger
    {
        private readonly BaseService<LogEvento> _dbLogService;

        public DBEventLogger(ApiDBContext dBContext, IAppSettingsModule appSettingsModule) 
        {
            _dbLogService = new BaseService<LogEvento>(dBContext, appSettingsModule);
        }

        public bool SaveLog(LogEvento ex)
        {
            bool guardado = false;
            var response = _dbLogService.CrearLog(ex);
            if (response.Result.Data != null)
            {
                guardado = true;
            }
            return guardado;
        }
    }
}
