using System;

namespace Planilla.DTO.Planilla
{
    public class ResumenBoletaPagoDTO
    {
        public int DetallePlanillaId { get; set; }
        public decimal SueldoNeto { get; set; }
        public string Descripcion { get; set; }
        public string FechaCorte { get; set; }
    }
}
