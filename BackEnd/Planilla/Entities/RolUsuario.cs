using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class RolUsuario
    {
        public int RolUsuarioId { get; set; }
        public int? UsuarioId { get; set; }
        public int? RolId { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
