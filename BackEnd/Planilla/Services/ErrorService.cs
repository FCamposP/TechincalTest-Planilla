using Planilla.Entities;
using System;
using Planilla.Abstractions;

namespace Planilla.Services
{
    public class ErrorService
    {

        public ILogErrorger Logger { get; set; }
        public void RegisterException(Exception ex)
        {
            Logger.RegisterException(ex);
        }
        public void RegisterException(LogError ex)
        {
            Logger.RegisterException(ex);
        }

        public void RegisterException(Exception ex, ILogErrorger LogErrorger)
        {
            Logger = LogErrorger;
            Logger.RegisterException(ex);
        }

        public void RegisterException(Exception ex, ILogErrorger mainLogErrorger, ILogErrorger segundoLogErrorger)
        {
            //Logger = mainLogErrorger;
            //if (RegisterException(ex))
            //{
            //    return true;
            //}
            //else
            //{
            //    Logger = segundoLogErrorger;
            //    return RegisterException(ex);
            //}
        }
    }
}
