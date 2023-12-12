using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class EncabezadoPlanilla
    {
        public EncabezadoPlanilla()
        {
            DetallePlanilla = new HashSet<DetallePlanilla>();
        }

        public int EncabezadoPlanillaId { get; set; }
        public int? PeriodoId { get; set; }
        public int? EstadoPlanillaId { get; set; }
        public string Descripcion { get; set; }
        public bool? EnviarCorreo { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual EstadoPlanilla EstadoPlanilla { get; set; }
        public virtual Periodo Periodo { get; set; }
        public virtual ICollection<DetallePlanilla> DetallePlanilla { get; set; }
    }
}
