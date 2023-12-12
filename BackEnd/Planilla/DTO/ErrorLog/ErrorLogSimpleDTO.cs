using System;

namespace Planilla.DTO.LogError
{
    public class LogErrorSimpleDTO
    {
        public int LogErrorId { get; set; }
        public string Modulo { get; set; }
        public string Message { get; set; }
        public DateTime Creado { get; set; }
    }
}
