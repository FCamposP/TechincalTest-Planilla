﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            RolPermiso = new HashSet<RolPermiso>();
            RolUsuario = new HashSet<RolUsuario>();
        }

        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }
        public bool? EsSuperUsuario { get; set; }

        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
        public virtual ICollection<RolUsuario> RolUsuario { get; set; }
    }
}
