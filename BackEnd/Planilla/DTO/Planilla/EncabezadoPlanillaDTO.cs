using Planilla.Entities;
using System.Collections.Generic;
using System;

namespace Planilla.DTO.Planilla
{
    public class EncabezadoPlanillaDTO
    {
        public int EncabezadoPlanillaId { get; set; }
        public int? PeriodoId { get; set; }
        public string Descripcion { get; set; }
        public string? Estado { get; set; }
        public bool? EnviarCorreo { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? Activo { get; set; }
        public DateTime Creado { get; set; }
    }
}
