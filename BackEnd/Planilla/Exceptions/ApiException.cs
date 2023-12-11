using System;

namespace Planilla.Exceptions
{
    public class ApiException:Exception
    {
        public int ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public Object ErrorContent { get; private set; }

        public ApiException() { }

        public  ApiException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(int errorCode, string message, object errorContent) : base(message)
        {
            ErrorCode = errorCode;
            ErrorContent = errorContent;
        }

        public ApiException(int errorCode, string message, int statusCode, object errorContent) :base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            ErrorContent = errorContent;
        }
    }
}
