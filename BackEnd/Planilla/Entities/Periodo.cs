using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class Periodo
    {
        public Periodo()
        {
            EncabezadoPlanilla = new HashSet<EncabezadoPlanilla>();
        }

        public int PeriodoId { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual ICollection<EncabezadoPlanilla> EncabezadoPlanilla { get; set; }
    }
}
