using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.Entities
{
    public partial class Componente
    {
        public Componente()
        {
            InversePadre = new HashSet<Componente>();
            RolPermiso = new HashSet<RolPermiso>();
        }

        public int ComponenteId { get; set; }
        public int? TipoComponenteId { get; set; }
        public int? PadreId { get; set; }
        public string Nombre { get; set; }
        public string NombreMostrar { get; set; }
        public string Descripcion { get; set; }
        public int? Orden { get; set; }
        public string Url { get; set; }
        public bool? EsFrontOffice { get; set; }
        public string Icon { get; set; }
        public bool? Activo { get; set; }
        public int Creador { get; set; }
        public DateTime Creado { get; set; }
        public int? Modificador { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual Componente Padre { get; set; }
        public virtual TipoComponente TipoComponente { get; set; }
        public virtual ICollection<Componente> InversePadre { get; set; }
        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
    }
}
