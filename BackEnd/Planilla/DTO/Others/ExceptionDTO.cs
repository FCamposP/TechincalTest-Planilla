using System;
using System.Diagnostics;

namespace Planilla.DTO.Others
{
    public class ExceptionDTO
    {
        public string ModuleCode { get; set; }
        public string EnvironmentCode { get; set; }
        public string ErrorSourceIPAddress { get; set; }
        public string AdditionalInformation { get; set; }
        public int HResult { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? TargetSite { get; set; }
        public ExceptionDTO? InnerException { get; set; }

        public ExceptionDTO(string moduleCode, string environmentCode, string errorSourceIPAddress, string additionalInformation, int hResult, string message, string stackTrace, string source, string targetSite)
        {
            ModuleCode = moduleCode;
            EnvironmentCode = environmentCode;
            ErrorSourceIPAddress = errorSourceIPAddress;
            AdditionalInformation = additionalInformation;
            HResult = hResult;
            Message = message;
            StackTrace = stackTrace;
            Source = source;
            TargetSite = targetSite;
        }

        public ExceptionDTO()
        {
            ModuleCode = String.Empty;
            EnvironmentCode = String.Empty;
            ErrorSourceIPAddress = String.Empty;
            AdditionalInformation = String.Empty;
            HResult = 0;
            Message = String.Empty;
            StackTrace = String.Empty;
            Source = String.Empty;
            TargetSite = String.Empty;
        }

        public static explicit operator ExceptionDTO(Exception ex)
        {
            var ModuleCode = "";
            var EnvironmentCode = "";
            ExceptionDTO value = new ExceptionDTO()
            {
                ModuleCode = ModuleCode,
                EnvironmentCode = EnvironmentCode,
                HResult = ex.HResult,
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

                value.InnerException = (ExceptionDTO)innerEx;
            }
            return value;
        }

        public override string ToString()
        {
            if (InnerException != null)
            {
                return InnerException.Message ?? Message;
            }
            else
            {
                return Message;
            }
        }
    }
}
