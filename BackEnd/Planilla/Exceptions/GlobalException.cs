using Planilla.DTO.Others;
using System;

namespace Planilla.Exceptions
{
    public class GlobalException:Exception
    {
        public override string Message { get; }
        public override string  StackTrace { get; }

        public GlobalException(ExceptionDTO ex)
        {
            Message= ex.Message;
            StackTrace = ex.StackTrace??"";
            Source = ex.Source;
            if (ex.InnerException != null)
            {
                Message += " | " + ex.InnerException.Message ?? "";
                StackTrace += " | " + ex.InnerException.StackTrace ?? "";
                Source += " | " + ex.InnerException.Source ?? "";

            }
        }
    }
}
