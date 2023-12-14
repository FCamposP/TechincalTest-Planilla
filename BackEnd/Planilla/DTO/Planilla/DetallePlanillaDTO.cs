using System;

namespace Planilla.DTO.Planilla
{
    public class DetallePlanillaDTO
    {
        public int DetallePlanillaId { get; set; }
        public int EncabezadoPlanillaId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal Salario { get; set; }
        public decimal DescuentoIsss { get; set; }
        public decimal DescuentoAfp { get; set; }
        public decimal DescuentoRenta { get; set; }
        public decimal OtrosDescuentos { get; set; }
        public decimal? SueldoNeto { get; set; }
        public bool? Activo { get; set; }
        public string CodigoEmpleado { get; set; }
        public DateTime Creado { get; set; }
    }
}
