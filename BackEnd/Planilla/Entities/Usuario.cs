using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Notificacion = new HashSet<Notificacion>();
            RolUsuario = new HashSet<RolUsuario>();
        }

        public int UsuarioId { get; set; }
        public int? EmpleadoId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? IntentosFallidos { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<RolUsuario> RolUsuario { get; set; }
    }
}
