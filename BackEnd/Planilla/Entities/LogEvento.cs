using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class LogEvento
    {
        public int LogEventoId { get; set; }
        public string Modulo { get; set; }
        public string Entorno { get; set; }
        public string IpOrigen { get; set; }
        public string TipoLog { get; set; }
        public string Mensaje { get; set; }
        public DateTime Creado { get; set; }
    }
}
