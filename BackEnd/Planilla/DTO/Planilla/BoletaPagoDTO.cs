namespace Planilla.DTO.Planilla
{
    public class BoletaPagoDTO
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
        public string Descripcion { get; set; }
        public string FechaCorte { get; set; }
        public string CodigoEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombrePuesto { get; set; }
    }
}
