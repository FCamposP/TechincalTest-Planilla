using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class RolDTO
    {
        /// <summary>
        /// Identificador de un rol de usuario
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Descripción del registro
        /// </summary>
        public string? Descripcion { get; set; }

        public bool? EsSuperUsuario { get; set; }

        /// <summary>
        /// Indica si un registro esta activo en el sistema
        /// </summary>
        public bool? Activo { get; set; }

        /// <summary>
        /// Id de usuario creador del registro
        /// </summary>
        public int Creador { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime Creado { get; set; }

        public virtual IList<RolPermisoDTO> RolPermisos { get; set; } = new List<RolPermisoDTO>();

        public virtual IList<RolUsuarioDTO> RolUsuarios { get; set; } = new List<RolUsuarioDTO>();
    }
}
