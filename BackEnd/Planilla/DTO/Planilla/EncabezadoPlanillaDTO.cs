using Planilla.Entities;
using System.Collections.Generic;
using System;

namespace Planilla.DTO.Planilla
{
    public class EncabezadoPlanillaDTO
    {
        public int EncabezadoPlanillaId { get; set; }
        public int? PeriodoId { get; set; }
        public int? EstadoPlanillaId { get; set; }
        public string Descripcion { get; set; }
        public string NombreEstado { get; set; }
        public string DescripcionPeriodo { get; set; }
        public bool? Habilitado { get; set; }

        public DateTime? FechaCorte { get; set; }
        public bool? EnviarCorreo { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? Activo { get; set; }
        public DateTime Creado { get; set; }
        public virtual IList<DetallePlanillaDTO> DetallePlanilla { get; set; } = new List<DetallePlanillaDTO>();

    }
}
