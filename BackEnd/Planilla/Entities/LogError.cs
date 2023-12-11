using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class LogError
    {
        public int LogErrorId { get; set; }
        public string Modulo { get; set; }
        public string Entorno { get; set; }
        public string IpOrigen { get; set; }
        public string InformacionAdicional { get; set; }
        public int? Hresult { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public int? InnerExceptionHresult { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionSource { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public string InnerExceptionTargetSite { get; set; }
        public DateTime Creado { get; set; }

        public static explicit operator LogError(Exception ex)
        {
            LogError value = new LogError()
            {
                Hresult = ex.HResult,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                TargetSite = ex.TargetSite?.ToString()
            };
            if (ex.InnerException != null)
            {
                Exception innerEx = ex.InnerException;
                while (innerEx.InnerException != null)
                {
                    innerEx = innerEx.InnerException;
                }
                value.InnerExceptionMessage = innerEx.Message;
                value.InnerExceptionSource = innerEx.Source;
                value.InnerExceptionStackTrace = innerEx.StackTrace;
                value.InnerExceptionTargetSite = innerEx.TargetSite?.ToString();
            }

            return value;
        }
    }
}
