using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class TipoDatoDTO
    {
        public int TipoDatoId { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Formato { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
    }
}
