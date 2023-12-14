namespace Planilla.DTO
{
    public class EstadoPlanillaDTO
    {
        public int EstadoPlanillaId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
    }
}
