using System;

namespace Planilla.DTO.ConfiguracionGlobal
{
    public class ConfiguracionGlobalDTO
    {
        public int ConfiguracionId { get; set; }
        public string? Codigo { get; set; }
        public string? Valor { get; set; }
        public string? NombreAmostrar { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public bool? EsImagen { get; set; }
    }
}
