using System;

namespace Planilla.DTO
{
    public class ColumnaExcelDTO
    {
        public int ColumnaExcelId { get; set; }
        public int? TipoDatoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreTipoDato  { get; set; }
        public bool? Activo { get; set; }
        public DateTime Creado { get; set; }

    }
}
