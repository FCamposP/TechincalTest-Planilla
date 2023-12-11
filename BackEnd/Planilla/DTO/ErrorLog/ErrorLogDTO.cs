using System;

namespace Planilla.DTO.LogError
{
    public class LogErrorDTO
    {
        public int LogErrorId { get; set; }
        public string Modulo { get; set; }
        public string Entorno { get; set; }
        public string InformacionAdicional { get; set; }
        public int? Hresult { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public DateTime Creado { get; set; }
    }
}
