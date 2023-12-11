using System;

namespace Planilla.Exceptions
{
    public class EmptyInputException:Exception
    {
        public override string Message { get; }

        public EmptyInputException(string message)
        {
            Message = message;
            HResult = 1;
        }

        public EmptyInputException()
        {
            Message = "El parámetro de entrada no puede ser nulo.";
            HResult = 1;
        }
    }
}
