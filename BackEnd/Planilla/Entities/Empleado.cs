using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class Empleado
    {
        public Empleado()
        {
            DetallePlanilla = new HashSet<DetallePlanilla>();
            Usuario = new HashSet<Usuario>();
        }

        public int EmpleadoId { get; set; }
        public string Codigo { get; set; }
        public int? PuestoId { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual Puesto Puesto { get; set; }
        public virtual ICollection<DetallePlanilla> DetallePlanilla { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
