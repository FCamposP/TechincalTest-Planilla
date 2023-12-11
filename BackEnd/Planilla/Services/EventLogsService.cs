using Planilla.Abstractions;
using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Services
{
    public class EventLogsService
    {
        public IEventLogger Logger { get; set; }
        public bool RegisterException(LogEvento ex)
        {
            return Logger.SaveLog(ex);
        }
    }
}
