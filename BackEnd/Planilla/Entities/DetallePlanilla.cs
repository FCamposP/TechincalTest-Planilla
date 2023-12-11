using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class DetallePlanilla
    {
        public int DetallePlanillaId { get; set; }
        public int? EncabezadoPlanillaId { get; set; }
        public DateTime FechaCorte { get; set; }
        public decimal DescuentoIsss { get; set; }
        public decimal DescuentoAfp { get; set; }
        public decimal DescuentoRenta { get; set; }
        public decimal DescuentoOtros { get; set; }
        public decimal? SueldoNeto { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual EncabezadoPlanilla EncabezadoPlanilla { get; set; }
    }
}
