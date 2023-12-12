using System;

namespace Planilla.DTO
{
    public class PeriodoDTO
    {
        public int PeriodoId { get; set; }
        public int? TipoPeriodoId { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoPeriodoNombre { get; set; }
    }
}
