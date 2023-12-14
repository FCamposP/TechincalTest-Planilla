using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; }
        public int PuestoId { get; set; }
        public string? NombrePuesto { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime? Creado { get; set; }
    }

}
