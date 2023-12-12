using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class ColumnaExcel
    {
        public int ColumnaExcelId { get; set; }
        public int? TipoDatoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Orden { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual TipoDato TipoDato { get; set; }
    }
}
